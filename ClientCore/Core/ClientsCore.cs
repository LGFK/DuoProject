﻿using ClientCore.Errors;
using ClientCore.Helpes;
using ClientCore.Rusults;
using ClientCore.Model;
using ComandLibrary;
using CommunicationLibrary;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientCore.Core;
public class ClientsCore
{
    private IPEndPoint _endpoint;
    //private ClientCache _cache;
    private const int MaxConnectionAttempts = 3;
    private const int ConnectionRetryDekayMilliseconds = 7000;

    public ClientsCore()
    {
       // _cache = new ClientCache();
        _endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
    }

    public async Task<RequestResult> SendRequestAsync(string? message, ComandsLib comandsLib)
    {
        int attemps = 0;
        List<Error> errors = new List<Error>();
        while (attemps < MaxConnectionAttempts)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(_endpoint);

                    var networkSteem = client.GetStream();
                    await SendRequest(networkSteem, comandsLib);

                    var requestToReceive = await ReceiveResponse(networkSteem);
                    var res = DeserializationObjectFromServer(requestToReceive);
                    return RequestResult.Create(res);
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

            attemps++;

            if (attemps > MaxConnectionAttempts)
            {
                await Task.Delay(ConnectionRetryDekayMilliseconds);
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

        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        var requestSizeBytes = BitConverter.GetBytes(responseBytes.Length);

        await networkSteem.WriteAsync(requestSizeBytes, 0, requestSizeBytes.Length);
        await networkSteem.WriteAsync(responseBytes, 0, responseBytes.Length);
    }

    private async Task<byte[]> ReceiveResponse(NetworkStream networkSteem)
    {
        byte[] buffer = new byte[4];
        await networkSteem.ReadAsync(buffer, 0, buffer.Length);
        var responseSize = BitConverter.ToInt32(buffer, 0);

        var responseBytes = new byte[responseSize];
        var bytesRead = 0;

        while (bytesRead < responseSize)
        {
            bytesRead += await networkSteem.ReadAsync(responseBytes, bytesRead, responseSize - bytesRead);
        }
        return responseBytes;
    }

    private RequestResponseBase DeserializationObjectFromServer(byte[] requestToReceive)
    {
        if (requestToReceive is null)
        {
            return new RequestResponseBase() { Command = ComandsLib.ERROR };
        }

        string jsonToReceive = Encoding.UTF8.GetString(requestToReceive);
        var comand = JsonConvert.DeserializeObject<RequestResponseBase>(jsonToReceive);

        if (comand is null)
        {
            return new RequestResponseBase() { Command = ComandsLib.ERROR };
        }

        RequestResponseBase resultChoise = ChoiseCommand(comand.Command, jsonToReceive);
        return resultChoise;
    }

    private RequestResponseBase ChoiseCommand(ComandsLib comandsLib, string? jsonToReceive)
    {
        if (jsonToReceive is null)
        {
            return new RequestResponseBase() { Command = ComandsLib.ERROR };
        }

        var res = new RequestResponseBase();
        switch (comandsLib)
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
        //_cache.Add(comandsLib.ToString(), res);
        SaveInJson(res, comandsLib);
        return res;
    }

    private void SaveInJson(RequestResponseBase books, ComandsLib comandsLib)
    {

    }
}



