using ClientCore.Errors;
using ClientCore.Helpes;
using ClientCore.Interfaces;
using ClientCore.Model;
using ClientCore.Rusults;
using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using NetworkSerializationLib;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;

namespace ClientCore.Core;
public class ClientsCore 
{
    private IPEndPoint _endpoint;
    private const int MaxConnectionAttempts = 3;
    private const int ConnectionRetryDekaMilliseconds = 7000;

    public ClientsCore()
    {
        _endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
    }

    public async Task<RequestResult> SendRequestAsync(ComandsLib comandsLib, string? message = null)
    {
        int attempts = 0;
        List<Error> errors = new List<Error>();
        while (attempts < MaxConnectionAttempts)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(_endpoint);

                    var networkSteam = client.GetStream();
                    await SendRequest(networkSteam, comandsLib);

                    var requestToReceive = await JsonReceiveResponse.Receive(networkSteam);
                    var res = DeserializationObjectFromServer(requestToReceive);

                    return RequestResult.Create(res.Value);
                }
            }
            catch (SocketException)
            {
                //Failed to establish connection to the server.
                errors.Add(Error.InvalidInput);
            }
            catch
            {
                //An error occureed while sending/receiving the reques.
                errors.Add(Error.InvalidInput);
            }

            attempts++;

            if (attempts >= MaxConnectionAttempts)
            {
                await Task.Delay(ConnectionRetryDekaMilliseconds);
            }
        }
        errors.Add(Error.ConnectionTimeout);
        return RequestResult.Failure(errors.ToArray());
    }
    public async Task<RequestResult<NetworkStream>> Connected()//1
    {
        int attemps = 0;
        List<Error> errors = new List<Error>();
        while (attemps <= MaxConnectionAttempts)
        {
            try
            {
                var client = new TcpClient();
                await client.ConnectAsync(_endpoint);
                var networkStream = client.GetStream();
                return RequestResult.Create(networkStream);
            }
            catch (SocketException)
            {
                //need chage error message
                errors.Add(Error.InvalidInput);//Failed to establish connection to the server.
            }
            catch
            {
                errors.Add(Error.InvalidInput);//An error occureed while sending/receiving the reques.
            }
        }
        attemps++;

        if (attemps >= MaxConnectionAttempts)
        {
            await Task.Delay(ConnectionRetryDekaMilliseconds);
        }

        errors.Add(Error.ConnectionTimeout);
        return (RequestResult<NetworkStream>)RequestResult.Failure(errors.ToArray());
    }

    public async Task<RequestResult<RequestResponseBase>> SendResultAsync(ComandsLib comands, NetworkStream network, string? message = null)//2
    {
        if (ComandsLib.EditBook == comands)
        {
            return (RequestResult<RequestResponseBase>)RequestResult.Failure(Error.InvalidInput);
        }

        await SendRequest(network, comands);
        var requestToReceive = await JsonReceiveResponse.Receive(network);
        var resultDeserilize = DeserializationObjectFromServer(requestToReceive);
        return RequestResult.Create(resultDeserilize.Value);
    }

    public async Task<RequestResult> EditBook(NetworkStream network, Book book)//or 2
    {
        await SendRequestEditBook(network, ComandsLib.EditBook, book);//need add mb server chek return Error or null
        return RequestResult.Success();
    }

    private async Task SendRequest(NetworkStream networkSteem, ComandsLib comandsLib)
    {
        var response = new ClientRequest
        {
            TimesTamp = TimesTamp.GetTimesTamp(),
            Command = comandsLib,
            Message = string.Empty,
        };

        await JsonResponseWrite.Write(networkSteem, response);
    }

    private async Task SendRequestEditBook(NetworkStream networkSteem, ComandsLib comandsLib, Book book)
    {
        var jsonEditBook = JsonConvert.SerializeObject(book);
        var response = new ClientRequest
        {
            TimesTamp = TimesTamp.GetTimesTamp(),
            Command = comandsLib,
            Message = jsonEditBook,
        };

        await JsonResponseWrite.Write(networkSteem, response);
    }

    public RequestResult<RequestResponseBase> DeserializationObjectFromServer(string requestToReceive)
    {
        if (requestToReceive is null)
        {
            return RequestResult.Failure<RequestResponseBase>(Error.NullValue);
        }

        return RequestResult.Create(requestToReceive)
            .Map(jsonToReceive =>
            {
                var command = JsonConvert.DeserializeObject<RequestResponseBase>(jsonToReceive);

                if (command is null)
                {
                    return new RequestResponseBase() { Command = ComandsLib.ERROR };
                }

                return ChoiceCommand(command.Command, jsonToReceive);
            });
    }

    public RequestResponseBase ChoiceCommand(ComandsLib commandsLib, string? jsonToReceive)
    {
        if (jsonToReceive is null)
        {
            return new RequestResponseBase() { Command = ComandsLib.ERROR };
        }

        var res = new RequestResponseBase();
        switch (commandsLib)
        {
            case ComandsLib.GetAllBooks:
                res = DataBook.Books(jsonToReceive);
                break;
            case ComandsLib.GetAllUsers:
                res = DataUser.Users(jsonToReceive);
                break;
            case ComandsLib.GetFiveBestBooks:
                break;
            case ComandsLib.ERROR:
                return new RequestResponseBase() { Command = ComandsLib.ERROR };
            default:
                break;
        }
        //SaveInJson(res, commandsLib);
        return res;
    }
}