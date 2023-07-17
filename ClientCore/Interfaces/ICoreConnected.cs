using ClientCore.Rusults;
using System.Net.Sockets;

namespace ClientCore.Interfaces;
public interface ICoreConnected
{
    Task<RequestResult<NetworkStream>> Connected();
}
