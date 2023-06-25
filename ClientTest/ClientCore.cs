using ComandLibrary;
using CommunicationLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest;
internal class ClientCore
{
    private TcpClient _tcpClient;
    private byte[] _buffer = new byte[4];
    private IPEndPoint _endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
    private ClientCache _cache;
    public ClientCore()
    {
        _tcpClient = new TcpClient();
        _cache = new ClientCache();
    }
    public async Task<byte[]> SendRequestAsync(string? message)
    {
/*        if(!_tcpClient.Connected)
        {
            await _tcpClient.ConnectAsync(_endPoint);
        }
        var networkStream = _tcpClient.GetStream();
        await SendRequest(networkStream);

        var requestToReceive = await ReceiveResponse(networkStream);

        return requestToReceive; */

        using (var client = new TcpClient())
        {
            await client.ConnectAsync(_endPoint);
            if(client.Connected)
            {
                var netwokStream = client.GetStream();
                await SendRequest(netwokStream);
                var requestToReceive = await ReceiveResponse(netwokStream);
                return requestToReceive;
            }
        }
        return Array.Empty<byte>();
    }
    private async Task SendRequest(NetworkStream networkStream)
    {
        var response = new ClientRequest
        {
            /*Command = ComandsLib.GetAllBooks,*/
            Command = ComandsLib.GetAllUsers,
            //Message = "Hi server, how are you ?"
            Message = $"{TimesTamp.GetTimesTamp()}"
        };
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        var requestSizeBytes = BitConverter.GetBytes(responseBytes.Length);

        await networkStream.WriteAsync(requestSizeBytes, 0, requestSizeBytes.Length);
        await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
    }

    private async Task<byte[]> ReceiveResponse(NetworkStream networkStream)
    {
        Array.Clear(_buffer,0, _buffer.Length);
        await networkStream.ReadAsync(_buffer, 0, _buffer.Length);
        var responseSize = BitConverter.ToInt32(_buffer, 0);

        var responseBytes = new byte[responseSize];
        var bytesRead = 0;
        while (bytesRead < responseSize)
        {
            bytesRead += await networkStream.ReadAsync(responseBytes, bytesRead, responseSize - bytesRead);
        }
        //await networkStream.ReadAsync(responseBytes, 0, responseBytes.Length);
        return responseBytes;
    }
}
