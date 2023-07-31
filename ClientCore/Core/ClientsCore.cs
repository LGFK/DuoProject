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

    public async Task<RequestResult<NetworkStream>> Connected()//1
    {
        int attempts = 0;
        List<Error> errors = new List<Error>();
        while (attempts <= MaxConnectionAttempts)
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
        attempts++;

        if (attempts >= MaxConnectionAttempts)
        {
            await Task.Delay(ConnectionRetryDekaMilliseconds);
        }

        errors.Add(Error.ConnectionTimeout);
        return (RequestResult<NetworkStream>)RequestResult.Failure(errors.ToArray());
    }

    public async Task<RequestResult<RequestResponseBase>> SendResultAsync(ComandsLib commands, NetworkStream network, string? message = null)//2
    {
        if (ComandsLib.EditBook == commands)
        {
            return (RequestResult<RequestResponseBase>)RequestResult.Failure(Error.InvalidInput);
        }

        await SendRequest(network, commands);
        var requestToReceive = await JsonReceiveResponse.Receive(network);
        var resultDesterilize = DeserializationObjectFromServer(requestToReceive);
        return RequestResult.Create(resultDesterilize.Value);
    }

    public async Task<RequestResult> EditBook(NetworkStream network, Book book)//or 2
    {
        await SendRequestEditBook(network, ComandsLib.EditBook, book);//need add mb server chek return Error or null
        return RequestResult.Success();
    }

    private async Task SendRequest(NetworkStream networkStem, ComandsLib commandsLib)
    {
        var response = new ClientRequest
        {
            TimesTamp = TimesTamp.GetTimesTamp(),
            Command = commandsLib,
            Message = string.Empty,
        };

        await JsonResponseWrite.Write(networkStem, response);
    }

    private async Task SendRequestEditBook(NetworkStream networkStream, ComandsLib commandsLib, Book book)
    {
        var jsonEditBook = JsonConvert.SerializeObject(book);
        var response = new ClientRequest
        {
            TimesTamp = TimesTamp.GetTimesTamp(),
            Command = commandsLib,
            Message = jsonEditBook,
        };

        await JsonResponseWrite.Write(networkStream, response);
    }

    public async Task AddBook(NetworkStream networkStream, Book book )
    {
        var jsonAddBook = JsonConvert.SerializeObject(book);
        var response = new ClientRequest
        {
            TimesTamp = TimesTamp.GetTimesTamp(),
            Command = ComandsLib.AddBook,
            Message = jsonAddBook,
        };

        await JsonResponseWrite.Write(networkStream, response);
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

/*public async Task<RequestResult> SendRequestAsync(ComandsLib commandsLib, string? message = null)
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
                await SendRequest(networkSteam, commandsLib);

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
}*/