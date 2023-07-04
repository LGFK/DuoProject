using ClientCore.Helpes;
using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using Newtonsoft.Json;

namespace ClientCore.Model;
public static class DataBook
{
    public static GetBookResponse Books(string? jsonToReceive)
    {
        var books = JsonConvert.DeserializeObject<GetBookResponse>(jsonToReceive!);
        if(books is null)
        {
            return new GetBookResponse() { Command = ComandsLib.ERROR };
        }

        return books.Books is not null ? books : new GetBookResponse() { Books = new List<Book>(), Command = ComandsLib.ERROR };
    }
}
/*        var books = JsonConvert.DeserializeObject<GetBookResponse>(jsonToReceive!);
        if (books is null)
        {
            return new RequestResponseBase() { Command = ComandsLib.ERROR };
        }
        TimesTamp.SaveTimesTamp(books.TimesTamp);
        return books;*/