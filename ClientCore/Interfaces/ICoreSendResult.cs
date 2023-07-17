using ClientCore.Rusults;
using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using System.Net.Sockets;

namespace ClientCore.Interfaces;
public interface ICoreSendResult
{
    Task<RequestResult<RequestResponseBase>> SendResultAsync(ComandsLib comand, NetworkStream networkStream, string? message = null);
}
