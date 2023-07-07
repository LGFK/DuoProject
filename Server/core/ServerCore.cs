using ClientCore.Core;
using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using NetworkSerializationLib;
using Newtonsoft.Json;
using Server.DbContextsShop;
using Server.Helper;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.core;
internal class ServerCore
{
    private readonly TcpListener _listener;
    private string? _port = "1448";
    private bool _isStart;
    private byte[] _buffer = new byte[4];
    private readonly DbBook _dbBook;
    private readonly DbUser _dbUser;
    //private WeatherApi _weatherApi;

    public ServerCore()
    {
        _listener = new TcpListener(IPAddress.Any, int.Parse(_port));
        _dbBook = new DbBook(DbOptions.GetOptions());
        _dbUser = new DbUser(DbOptions.GetOptions());
    }

    public void StartServer()
    {
        _listener.Start();
        _isStart = true;
        TimesTamp.SaveTimesTamp();
        _ = WaitForConnectionAsync();
    }

    private async Task WaitForConnectionAsync()
    {
        while (_isStart)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    public async Task HandleClientAsync(TcpClient client)
    {
        var networkStream = client.GetStream();

        var res = await JsonReceiveResponse.Receive(networkStream);
        //var res = await GetFromClient(networkStream);
        var clientRequest = JsonConvert.DeserializeObject<ClientRequest>(res);

        ChooseCommand(networkStream, clientRequest);
    }

    private void ChooseCommand(NetworkStream networkStream, ClientRequest? clientRequest)
    {
        if (clientRequest == null)
        {
            return;
        }
        if (clientRequest.TimesTamp == TimesTamp.GetTimesTamp())
        {
            SendDateIsCurrent(networkStream);
        }
        switch (clientRequest.Command)
        {
            case ComandsLib.GetAllBooks:
                AllBooks(networkStream);
                break;
            case ComandsLib.GetAllUsers:
                AllUsers(networkStream);
                break;
            case ComandsLib.GetFiveBestBooks:
                FiveBestBooks(networkStream, clientRequest);
                break;
            case ComandsLib.ApiWeather:
                ApiWeather(networkStream, clientRequest);
                break;
            default:
                break;
        }
    }

    private void SendDateIsCurrent(NetworkStream networkStream)
    {
        var response = new ClientRequest
        {
            Command = ComandsLib.Successful,
            Message = "The data is current",
        };
        //JsonResponseWrite(networkStream, response);
    }

    private void ApiWeather(NetworkStream networkStream, ClientRequest clientRequest)
    {
        //_weatherApi = new WeatherApi("./config/appsetings.json");
    }

    private void FiveBestBooks(NetworkStream networkStream, ClientRequest clientRequest)
    {
        if (clientRequest == null)
        {
            return;
        }

        if (clientRequest.Message == null)
        {
            return;
        }

        List<Book> books = _dbBook.GetTopFiveGanre(clientRequest.Message);

        var response = new GetBookResponse
        {
            Books = books,
            Command = ComandsLib.GetFiveBestBooks,
            TimesTamp = TimesTamp.GetTimesTamp(),
        };

        //JsonResponseWrite(networkStream, response);
    }

    private void AllUsers(NetworkStream networkStream)
    {
        List<User> users = _dbUser.GetAllUsers();

        var response = new UsersResponse
        {
            Users = users,
            Command = ComandsLib.GetAllUsers,
            TimesTamp = TimesTamp.GetTimesTamp(),
        };

        //JsonResponseWrite(networkStream, response);
    }

    private async void AllBooks(NetworkStream networkStream)
    {
        List<Book> books = _dbBook.GetAllBooks();

        var response = new GetBookResponse
        {
            Books = books,
            Command = ComandsLib.GetAllBooks,
            TimesTamp = TimesTamp.GetTimesTamp(),
        };

        await JsonResponseWrite.Write(networkStream,response);
        //JsonResponseWrite(networkStream, response);
    }
}


// до бази додати окрему таблицю count і окрему додати жанр додати чек . паблішер, автор. 



// створити окрему бібліотеку із записом і читанням і зєднати із клієнтом 
/*    private void JsonResponseWrite(NetworkStream networkStream, RequestResponseBase response)
    {
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        _buffer = BitConverter.GetBytes(responseBytes.Length);

        networkStream.Write(_buffer, 0, _buffer.Length);
        networkStream.Write(responseBytes, 0, responseBytes.Length);
    }*/

/*    private async Task<string> GetFromClient(NetworkStream networkStream)
    {
        await networkStream.ReadAsync(_buffer, 0, _buffer.Length);
        int reqSize = BitConverter.ToInt32(_buffer, 0);
        byte[] requestBuffer = new byte[reqSize];
        int bytesRead = 0;

        while (bytesRead < reqSize)
        {
            bytesRead += await networkStream.ReadAsync(requestBuffer, bytesRead, reqSize - bytesRead);
        }

        return Encoding.UTF8.GetString(requestBuffer);
    }*/
//Get From client
/*        await networkStream.ReadAsync(_buffer, 0, _buffer.Length);
        int reqSize = BitConverter.ToInt32(_buffer, 0);
        _buffer = new byte[reqSize];
        await networkStream.ReadAsync(_buffer, 0, reqSize);
        return Encoding.UTF8.GetString(_buffer);*/

/*        int bytesRead= await networkStream.ReadAsync(_buffer, 0, _buffer.Length);
        int reqSize = BitConverter.ToInt32(_buffer, 0);

        _buffer = new byte[reqSize];
        bytesRead = await networkStream.ReadAsync(_buffer, 0, reqSize);

        while(bytesRead < reqSize)
        {
            int remainingBytes = reqSize - bytesRead;
            bytesRead += await networkStream.ReadAsync(_buffer, bytesRead, remainingBytes);
        }
        return Encoding.UTF8.GetString(_buffer);*/

/*        //Send MSG
        DbBook db = new DbBook(DbOptions.GetOptions());
        List<Book> ress = db.GetAllBooks();

        //var networkStream = client.GetStream();
        var jsonToSend = JsonConvert.SerializeObject(res);
        var responseToSend = Encoding.UTF8.GetBytes(jsonToSend);
        _buffer = BitConverter.GetBytes(responseToSend.Length);

        networkStream.Write(_buffer, 0, _buffer.Length);
        networkStream.Write(responseToSend, 0, responseToSend.Length);

        client.Dispose();*/

/*if (clientRequest.Command == ComandsLib.GetAllBooks)
           {
               DbBook db = new DbBook(DbOptions.GetOptions());
               List<Book> books = db.GetAllBooks();

               var response = new GetBookResponse
               {
                   Books = books,
                   Command = ComandsLib.Successful,
               };
               var jsonResponse = JsonConvert.SerializeObject(response);
               var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
               _buffer = BitConverter.GetBytes(responseBytes.Length);

               networkStream.Write(_buffer, 0, _buffer.Length);
               networkStream.Write(responseBytes, 0, responseBytes.Length);
           }*/