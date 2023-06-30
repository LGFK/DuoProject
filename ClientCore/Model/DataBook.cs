using ClientCore.Helpes;
using ComandLibrary;
using CommunicationLibrary;
using Newtonsoft.Json;

namespace ClientCore.Model;
public static class DataBook
{
    public static RequestResponseBase AllBooks(string? jsonToReceive)
    {
        var books = JsonConvert.DeserializeObject<GetBookResponse>(jsonToReceive!);
        if (books is null)
        {
            return new RequestResponseBase() { Command = ComandsLib.ERROR };
        }
        TimesTamp.SaveTimesTamp(books.TimesTamp);
        return books;
    }
}
