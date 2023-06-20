using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using Newtonsoft.Json;
using Server.DbContextsShop;
using Server.Helper;
using Server.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.core;
internal class ServerCore
{
    private TcpListener _listener;
    private string? _port = "1448";
    private bool _isStart;
    private byte[] _buffer = new byte[4];

    public ServerCore() 
    {
        _listener = new TcpListener(IPAddress.Any,int.Parse(_port));

    }
    public void StartServer()
    {
        _listener.Start();
        _isStart= true;
        _ = WaitForConnectionAsync();
    }

    private async Task WaitForConnectionAsync()
    {
        while(_isStart)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _ = HeandleClientAsync(client);
        }
    }

    public async Task HeandleClientAsync(TcpClient client)
    {
        var networkStream = client.GetStream();
        //Get MSG from client

        var res = await GetFromClient(networkStream);
        var clientRequest = JsonConvert.DeserializeObject<ClientRequest>(res);
        //
        if(clientRequest != null)
        {
            if (clientRequest.Command == ComandsLib.GetAllBooks)
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
            }
        }
        

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
    }

    private async Task<string> GetFromClient(NetworkStream networkStream)
    {
        await networkStream.ReadAsync(_buffer, 0, _buffer.Length);
        int reqSize = BitConverter.ToInt32(_buffer, 0);
        _buffer = new byte[reqSize];
        await networkStream.ReadAsync(_buffer, 0, reqSize);
        return  Encoding.UTF8.GetString(_buffer);
    }
}

