using ComandLibrary;
using CommunicationLibrary;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientTest;
internal class ClientCore
{
    private TcpClient _tcpClient;
    private byte[] _buffer = new byte[4];
    private IPEndPoint _endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
    private ClientCache _cache;
    private const int MaxConnectionAttempts = 3;
    private const int ConnectionRetryDekayMilliseconds = 7000;
    public ClientCore()
    {
        _tcpClient = new TcpClient();
        _cache = new ClientCache();
    }
    public async Task WaitFroConnectionAsync()
    {

    }
    public async Task<RequestAsyncResult> SendRequestAsync(string? message)
    {
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
        var resultRequestAsync = new RequestAsyncResult();
        int attemps = 0;
        while (attemps < MaxConnectionAttempts)
        {
            try
            {
                using(var client = new TcpClient())
                {
                    await client.ConnectAsync(_endPoint);
                    client.ReceiveTimeout= ConnectionRetryDekayMilliseconds;

                    var networkStream = client.GetStream();
                    await SendRequest(networkStream,ComandsLib.GetAllBooks);

                    var requestToReceive = await ReceiveResponse(networkStream);
                    resultRequestAsync.Success = true;
                    resultRequestAsync.Data = requestToReceive;

                    return resultRequestAsync;
                }
            }
            catch (SocketException ex)
            {
                resultRequestAsync.ErrorMessage = $"Failed to establish connection to the server.{ex.Message}";
                resultRequestAsync.Success = false;
            }
            catch (Exception ex)
            {
                resultRequestAsync.ErrorMessage = $"An error occurred while sending/receiving the request.{ex.Message}";
                resultRequestAsync.Success = false;   
            }
            attemps++;
        }
        resultRequestAsync.ErrorMessage += "MaxConnectionAttempts";
        return resultRequestAsync;
    }
    private async Task SendRequest(NetworkStream networkStream, ComandsLib command)
    {
        var response = new ClientRequest
        {
            TimesTamp = TimesTamp.GetTimesTamp(),
            Command = command,
            Message = String.Empty,
        };
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        var requestSizeBytes = BitConverter.GetBytes(responseBytes.Length);

        await networkStream.WriteAsync(requestSizeBytes, 0, requestSizeBytes.Length);
        await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
    }
    private async Task<byte[]> ReceiveResponse(NetworkStream networkStream)
    {
        Array.Clear(_buffer, 0, _buffer.Length);
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


/*        if(!_tcpClient.Connected)
        {
            await _tcpClient.ConnectAsync(_endPoint);
        }
        var networkStream = _tcpClient.GetStream();
        await SendRequest(networkStream);

        var requestToReceive = await ReceiveResponse(networkStream);

        return requestToReceive; */