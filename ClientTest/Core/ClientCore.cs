using ClientTest.Helpers;
using ClientTest.Model;
using ClientTest.TestError;
using ClientTest.TestResult;
using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest.Core;
internal class ClientCore
{
    //private byte[] _buffer = new byte[4];
    private IPEndPoint _endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
    private ClientCache _cache;
    private const int MaxConnectionAttempts = 3;
    private const int ConnectionRetryDekayMilliseconds = 7000;
    public ClientCore()
    {
        _cache = new ClientCache();
        LoadingTmp();
    }

    public async Task<RequestResult<NetworkStream>> Connected()
    {
        int attempts = 0;
        while (attempts <= MaxConnectionAttempts)
        {
            try
            {
                var client = new TcpClient();
                await client.ConnectAsync(_endPoint);
                var networkStream = client.GetStream();
                return RequestResult.Success(networkStream);
            }
            catch (Exception)
            {

                return (RequestResult<NetworkStream>)RequestResult.Failure(Error.NullValue);
            }
        }
        return (RequestResult<NetworkStream>)RequestResult.Failure(Error.NullValue);
    }

    public async Task<RequestResult<RequestResponseBase>> SendResultAsync(ComandsLib comands, NetworkStream networkStream, string message)
    {
        //await SendRequest(networkStream, comands);
        await EditBook(networkStream, new Book());
        var requestToReceive = await ReceiveResponse(networkStream);
        var res = DeserilizeObjectFromServer(requestToReceive);
        return RequestResult.Success(res.Value);
    }

    private async Task SendRequest(NetworkStream networkStream, ComandsLib command,Book book)
    {
        var jsonBook = JsonConvert.SerializeObject(book);

        var response = new ClientRequest
        {
            TimesTamp = TimesTamp.GetTimesTamp(),
            Command = command,
            Message = jsonBook,
        };
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        var requestSizeBytes = BitConverter.GetBytes(responseBytes.Length);

        await networkStream.WriteAsync(requestSizeBytes, 0, requestSizeBytes.Length);
        await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
    }

    public async Task<RequestResult>EditBook(NetworkStream networkStream, Book book)
    {
        await SendRequest(networkStream, ComandsLib.EditBook, new Book() { Name = "Good books !222", Author = new Author() { Name = "awdawdawadwadw"} });
        return RequestResult.Success();
    }

    public async Task<RequestResult> SendRequestAsync(string? message, ComandsLib comand)
    {
        int attempts = 0;
        while (attempts < MaxConnectionAttempts)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync(_endPoint);
                    client.ReceiveTimeout = ConnectionRetryDekayMilliseconds;

                    var networkStream = client.GetStream();
                    await SendRequest(networkStream, comand);

                    var requestToReceive = await ReceiveResponse(networkStream);
                    var res = (RequestResult<RequestResponseBase>)DeserilizeObjectFromServer(requestToReceive);
                    return RequestResult.Create(res.Value);
                }
            }
            catch (SocketException ex)
            {
                //$"Failed to establish connection to the server. {ex.Message}";
            }
            catch (Exception ex)
            {
                //$"An error occurred while sending/receiving the request. {ex.Message}";
            }
            attempts++;
            if (attempts < MaxConnectionAttempts)
            {
                await Task.Delay(ConnectionRetryDekayMilliseconds);
            }
        }
        return RequestResult.Failure(Error.ConnectionTimeout);
    }
    private async Task SendRequest(NetworkStream networkStream, ComandsLib command)
    {
        var response = new ClientRequest
        {
            TimesTamp = TimesTamp.GetTimesTamp(),
            Command = command,
            Message = string.Empty,
        };
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        var requestSizeBytes = BitConverter.GetBytes(responseBytes.Length);

        await networkStream.WriteAsync(requestSizeBytes, 0, requestSizeBytes.Length);
        await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
    }
    private async Task<byte[]> ReceiveResponse(NetworkStream networkStream)
    {
        byte[] buffer = new byte[4];
        Array.Clear(buffer, 0, buffer.Length);
        await networkStream.ReadAsync(buffer, 0, buffer.Length);
        var responseSize = BitConverter.ToInt32(buffer, 0);

        var responseBytes = new byte[responseSize];
        var bytesRead = 0;
        while (bytesRead < responseSize)
        {
            bytesRead += await networkStream.ReadAsync(responseBytes, bytesRead, responseSize - bytesRead);
        }
        return responseBytes;
    }
    private RequestResult<RequestResponseBase> DeserilizeObjectFromServer(byte[] requestToReceive)
    {
        if (requestToReceive is null)
        {
            return (RequestResult< RequestResponseBase>)RequestResult.Failure(Error.NullValue);
        }

        string jsonToReceive = Encoding.UTF8.GetString(requestToReceive);
        var comand = JsonConvert.DeserializeObject<RequestResponseBase>(jsonToReceive);
        if (comand is null)
        {
            return (RequestResult< RequestResponseBase>) RequestResult.Failure(Error.NullValue);
        }

        RequestResponseBase resultChoise = ChoiseCommand(comand.Command, jsonToReceive);
        return RequestResult.Create(resultChoise);
    }
    private RequestResponseBase ChoiseCommand(ComandsLib comands, string? jsonToReceive)
    {
        if (jsonToReceive is null)
        {
            return new RequestResponseBase() { Command = ComandsLib.ERROR };
        }

        switch (comands)
        {
            case ComandsLib.GetAllBooks:
                var books = DataBooks.AllBooks(jsonToReceive);
                _cache.Add(comands.ToString(), books);
                SaveInJson(books, comands);
                return books;
            case ComandsLib.GetAllUsers:
                var users = DataUsers.Users(jsonToReceive);
                _cache.Add(comands.ToString(), users);
                SaveInJson(users, comands);
                return users;
            case ComandsLib.GetFiveBestBooks:
                var bestbooks = DataBooks.AllBooks(jsonToReceive);
                _cache.Add(comands.ToString(), bestbooks);
                SaveInJson(bestbooks, comands);
                break;
            case ComandsLib.Successful:
                //LoadingTmp();
                break;
            case ComandsLib.ERROR:
                return new RequestResponseBase() { Command = ComandsLib.ERROR, };
            default:
                break;
        }
        return new RequestResponseBase() { Command = ComandsLib.ERROR };
    }
    private void SaveInJson(RequestResponseBase bk, ComandsLib comandsLibNameFile)
    {
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        using (var sw = File.CreateText($"{comandsLibNameFile}.json"))
        {
            js.Serialize(sw, bk);
        }
    }
    public void LoadingTmp()
    {
        var us = LoadingJsnFile(ComandsLib.GetAllUsers);

        if (us is not null
            && !_cache.ContainsKey(ComandsLib.GetAllUsers.ToString()))
        {
            _cache.Add(ComandsLib.GetAllUsers.ToString(), us);
        }
    }
    private RequestResponseBase LoadingJsnFile(ComandsLib comands)
    {
        //add try-catch
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        if (File.Exists($"{comands}.json"))
        {
            using (var sr = File.OpenText($"{comands}.json"))
            {
                return (RequestResponseBase)js.Deserialize(sr, typeof(RequestResponseBase))!;
            }
        }
        return new RequestResponseBase() { Command = ComandsLib.ERROR };// need CHANGE !!
    }
}


/*        switch (comand.Command)
        {
            case ComandsLib.GetAllBooks:
                var books = JsonConvert.DeserializeObject<GetBookResponse>(jsonToReceive);
                _cache.Add(ComandsLib.GetAllBooks.ToString(), books);
                var bk = _cache.Get<GetBookResponse>(ComandsLib.GetAllBooks.ToString());
                if (bk is not null)
                {
                    SaveInJson(bk, ComandsLib.GetAllBooks);
                    TimesTamp.SaveTimesTamp(bk.TimesTamp);
                    return bk;
                }
                break;
            case ComandsLib.GetAllUsers:
                break;
            case ComandsLib.GetFiveBestBooks:
                break;
            case ComandsLib.Successful:
                LoadingTmp();
                break;
            case ComandsLib.ApiWeather:
                break;
            case ComandsLib.ERROR:
                break;
            default:
                break;
        }*/

// case ComandsLib.GetAllBooks:
// return;
/*                var books = JsonConvert.DeserializeObject<GetBookResponse>(jsonToReceive);
                _cache.Add(ComandsLib.GetAllBooks.ToString(), books);
                var bk = _cache.Get<GetBookResponse>(ComandsLib.GetAllBooks.ToString());
                if (bk is not null)
                {
                    SaveInJson(bk, ComandsLib.GetAllBooks);
                    TimesTamp.SaveTimesTamp(bk.TimesTamp);
                    return bk;
                }*/
/*return AllBooks(jsonToReceive);*/

/*private Dictionary<ComandsLib, Type> responseTypesMap = new Dictionary<ComandsLib, Type>()
    {
        { ComandsLib.GetAllBooks, typeof(GetBookResponse) },
    };*/
/*        if(!_tcpClient.Connected)
        {
            await _tcpClient.ConnectAsync(_endPoint);
        }
        var networkStream = _tcpClient.GetStream();
        await SendRequest(networkStream);

        var requestToReceive = await ReceiveResponse(networkStream);

        return requestToReceive; */


/* using (var client = new TcpClient())
 {
     int atterpts = 0;
     while (atterpts < MaxConnectionAttempts)
     {
         try
         {
             await client.ConnectAsync(_endPoint);
             var netwokStream = client.GetStream();
             await SendRequest(netwokStream);
             var requestToReceive = await ReceiveResponse(netwokStream);
             return requestToReceive;
         }
         catch (SocketException)
         {
             //ERROR1

         }
         catch (Exception )
         {

         }
         atterpts++;
         if (atterpts < MaxConnectionAttempts)
         {

             Thread.Sleep(ConnectionRetryDekayMilliseconds);
         }
     }

     if (client == null || !client.Connected)
     {
         //ERROR 3 no connection on server
     }
 }
 return Array.Empty<byte>();*/