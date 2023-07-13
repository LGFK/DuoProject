using ClientCore.Errors;
using ClientCore.Helpes;
using ClientCore.Model;
using ClientCore.Rusults;
using ComandLibrary;
using CommunicationLibrary;
using NetworkSerializationLib;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

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

    private RequestResult<RequestResponseBase> DeserializationObjectFromServer(string requestToReceive)
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

    private RequestResponseBase ChoiceCommand(ComandsLib commandsLib, string? jsonToReceive)
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
            case ComandsLib.Successful:
                break;
            case ComandsLib.ApiWeather:
                break;
            case ComandsLib.ERROR:
                return new RequestResponseBase() { Command = ComandsLib.ERROR };
            default:
                break;
        }
        //SaveInJson(res, commandsLib);
        return res;
    }

    private void SaveInJson(RequestResponseBase books, ComandsLib commandsLib)
    {

    }
}