using ClientTest.Helpers;
using ComandLibrary;
using CommunicationLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest.Model;
public static class DataBooks
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
