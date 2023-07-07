using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore.Core;
public static  class JsonReceiveResponse
{
    public static async Task<string> Receive(NetworkStream networkStream)
    {
        if(networkStream is null)
        {
            return string.Empty;
        }

        byte[] buffer = new byte[4];
        await networkStream.ReadAsync(buffer,0,buffer.Length);
        var responseSize = BitConverter.ToInt32(buffer, 0);

        var responseBytes = new byte[responseSize];
        var bytesRead = 0;

        while (bytesRead < responseBytes.Length)
        {
            bytesRead += await networkStream.ReadAsync(responseBytes, bytesRead, responseSize - bytesRead);
        }
        //return responseBytes;
        return Encoding.UTF8.GetString(responseBytes);
    }
}
