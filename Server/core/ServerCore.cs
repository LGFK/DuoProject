using ClientCore.Core;
using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using NetworkSerializationLib;
using Newtonsoft.Json;
using Server.DbContextsShop;
using Server.Helper;
using System.Net;
using System.Net.Sockets;

namespace Server.core;
/// <summary>
/// Цей клас виконує роль центральної логіки серверної частини, обробляючи запити від клієнтів та взаємодіючи з базою даних для отримання необхідної інформації.
/// </summary>
internal class ServerCore
{
    private readonly TcpListener _listener;
    private string? _port = "1448";
    private bool _isStart;
    private readonly DbBook _dbBook;
    private readonly DbUser _dbUser;
    //private WeatherApi _weatherApi;

    public ServerCore()
    {
        _listener = new TcpListener(IPAddress.Any, int.Parse(_port));
        _dbBook = new DbBook(DbOptions.GetOptions());
        _dbUser = new DbUser(DbOptions.GetOptions());
    }

    public void StartServer()
    {
        _listener.Start();
        _isStart = true;
        TimesTamp.SaveTimesTamp();
        _ = WaitForConnectionAsync();
    }

    private async Task WaitForConnectionAsync()
    {
        while (_isStart)
        {
            var client = await _listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client);
        }
    }

    public async Task HandleClientAsync(TcpClient client)
    {
        var networkStream = client.GetStream();

        var res = await JsonReceiveResponse.Receive(networkStream);
        var clientRequest = JsonConvert.DeserializeObject<ClientRequest>(res);

        ChooseCommand(networkStream, clientRequest);
    }

    private void ChooseCommand(NetworkStream networkStream, ClientRequest? clientRequest)
    {
        if (clientRequest == null)
        {
            return;
        }

        if (clientRequest.TimesTamp == TimesTamp.GetTimesTamp())
        {
            SendDateIsCurrent(networkStream);
        }

        switch (clientRequest.Command)
        {
            case ComandsLib.GetAllBooks:
                AllBooks(networkStream);
                break;
            case ComandsLib.GetAllUsers:
                AllUsers(networkStream);
                break;
            case ComandsLib.GetFiveBestBooks:
                FiveBestBooks(networkStream, clientRequest);
                break;
            case ComandsLib.ApiWeather:
                ApiWeather(networkStream, clientRequest);
                break;
            case ComandsLib.EditBook:
                EditBook(networkStream, clientRequest);
                break;
            default:
                break;
        }
    }

    private async void SendDateIsCurrent(NetworkStream networkStream)
    {
        var response = new ClientRequest
        {
            Command = ComandsLib.Successful,
            Message = "The data is current",
        };

        await JsonResponseWrite.Write(networkStream, response);
    }

    private void ApiWeather(NetworkStream networkStream, ClientRequest clientRequest)
    {
        //_weatherApi = new WeatherApi("./config/appsetings.json");
    }

    private async void FiveBestBooks(NetworkStream networkStream, ClientRequest clientRequest)
    {
        if (clientRequest.Message == null)
        {
            await JsonResponseWrite.Write(networkStream, response: new GetBookResponse() { Command = ComandsLib.ERROR });
            return;
        }

        List<Book> books = _dbBook.GetTopFiveGenre(clientRequest.Message);

        var response = new GetBookResponse
        {
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
/*        var response = new GetBookResponse
        {
            Books = books,
            Command = ComandsLib.GetFiveBestBooks,
            TimesTamp = TimesTamp.GetTimesTamp(),
        };*/

        await JsonResponseWrite.Write(networkStream, response);
    }

    private async void AllUsers(NetworkStream networkStream)
    {
        List<User> users = _dbUser.GetAllUsers();

        var response = new UsersResponse
        {
            Users = users,
            Command = ComandsLib.GetAllUsers,
            TimesTamp = TimesTamp.GetTimesTamp(),
        };

        await JsonResponseWrite.Write(networkStream, response);
    }

    private async void AllBooks(NetworkStream networkStream)
    {
        List<Book> books = _dbBook.GetAllBooks();

        var response = new GetBookResponse
        {
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

        await JsonResponseWrite.Write(networkStream, response);
    }

    private void EditBook(NetworkStream networkStream, ClientRequest clientRequest)
    {
        if (clientRequest == null)
        {
            return;
        }

        if (clientRequest.Message == null)
        {
            return;
        }
        var book = JsonConvert.DeserializeObject<Book>(clientRequest.Message);
        
        //need check null book
        _dbBook.EditBoks(book.Id, book);
    }
}