using CommunicationLibrary;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace NetworkSerializationLib;
public static class JsonResponseWrite
{
    public static async Task Write(NetworkStream networkStream, RequestResponseBase response)
    {
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        var buffer = BitConverter.GetBytes(responseBytes.Length);

        await networkStream.WriteAsync(buffer, 0, buffer.Length);
        await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
    }
}
