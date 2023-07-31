using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using Newtonsoft.Json;
using Server.Helper;

namespace Server.core;
partial struct BookLogic
{
    public static GetBookResponse AllBooks(List<Book> books)
    {
        var response = new GetBookResponse
        {
            Command = ComandsLib.GetAllBooks,
            TimesTamp = TimesTamp.GetTimesTamp(),
            Books = books.Select(b => new Book
            {
                Id = b.Id,
                Name = b.Name,
                NumberOfPages = b.NumberOfPages,
                TimeOfPublication = b.TimeOfPublication,
                Cost = b.Cost,
                PriceForSale = b.PriceForSale,
                Image = b.Image,
                CountBooks = new CountBooks
                {

                    Count = b.CountBooks?.Count ?? 0,
                },
                Publisher = new Publisher
                {
                    Id = b.PublisherId,
                    Name = b.Publisher?.Name ?? string.Empty,
                },
                Genre = new Genre
                {
                    Id = b.GenreId,
                    Name = b.Genre?.Name ?? string.Empty,
                },
                Author = new Author
                {
                    Id = b.AuthorId,
                    Name = b?.Author?.Name ?? string.Empty,
                }
            }).ToList(),
        };
        return response;
    }

    public static GetBookResponse FiveBestBooks(List<Book> books)
    {
        var response = new GetBookResponse
        {
            Command = ComandsLib.GetFiveBestBooks,
            TimesTamp = TimesTamp.GetTimesTamp(),
            Books = books.Select(b => new Book
            {
                Id = b.Id,
                Name = b.Name,
                NumberOfPages = b.NumberOfPages,
                TimeOfPublication = b.TimeOfPublication,
                Cost = b.Cost,
                PriceForSale = b.PriceForSale,
                Image = b.Image,
                CountBooks = new CountBooks
                {
                    Count = b.CountBooks?.Count ?? 0,
                },
                Publisher = new Publisher
                {
                    Id = b.PublisherId,
                    Name = b.Publisher?.Name ?? string.Empty,
                },
                Genre = new Genre
                {
                    Id = b.GenreId,
                    Name = b.Genre?.Name ?? string.Empty,
                },

            }).ToList(),
        };
        return response;
    }

    public static Book? AddBook(ClientRequest clientRequest)
    {
        if (!ValidatorClient(clientRequest))
        {
            return null;
        }

        var book = JsonConvert.DeserializeObject<Book>(clientRequest.Message!);
        if(book is null)
        {
            return null;
        }

        return book;
    }

    private static bool ValidatorClient(ClientRequest clientRequest)
    {
        if (clientRequest == null)
        {
            return false;
        }

        if (clientRequest.Message == null)
        {
            return false;
        }
        return true;
    }
}
