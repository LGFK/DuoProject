using ClientCore.Rusults;
using ModelsLibrary;
using System.Net.Sockets;

namespace ClientCore.Interfaces;
public interface ICoreEdit
{
    Task<RequestResult> EditBook(NetworkStream networkStream, Book book);
}
