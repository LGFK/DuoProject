using ComandLibrary;
using CommunicationLibrary;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace ClientTest;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private byte[] _buffer = new byte[4];
    private IPEndPoint _endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
    private ClientCache _cache;
    private TcpClient _client;
    private ClientCore _clientCore;
    public MainWindow()
    {
        InitializeComponent();
        _cache = new ClientCache();
        _client = new TcpClient();
        _clientCore = new ClientCore();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // TcpClient _client = new TcpClient();
        if (!_client.Connected)
        {
            _client.Connect(_endPoint);
        }

        var _networkStream = _client.GetStream();

        Write(_networkStream);

        //get info
        byte[] requestToReceive = Read(_networkStream);

        string jsonToReceive = Encoding.UTF8.GetString(requestToReceive);
        var comand = JsonConvert.DeserializeObject<RequestResponseBase>(jsonToReceive);

        //test cache

        if (comand != null)
        {
            switch (comand.Command)
            {
                case ComandsLib.GetAllBooks:
                    var books = JsonConvert.DeserializeObject<GetBookResponse>(jsonToReceive);
                    _cache.Add(ComandsLib.GetAllBooks.ToString(), books);
                    var bk = _cache.Get<GetBookResponse>(ComandsLib.GetAllBooks.ToString());
                    if (bk != null)
                    {
                        SaveInJson(bk);
                        foreach (var book in bk.Books)
                        {
                            ListBox.Items.Add(book.Name);
                        }
                    }

                    break;
                case ComandsLib.GetAllUsers:
                    var users = JsonConvert.DeserializeObject<UsersResponse>(jsonToReceive);
                    //логіка додавання в бокс
                    _cache.Add(ComandsLib.GetAllUsers.ToString(), users);
                    var us = _cache.Get<UsersResponse>(ComandsLib.GetAllUsers.ToString());
                    SaveInJson(us);
                    TimesTamp.SaveTimesTamp(us.TimesTamp);
                    if (us != null && us.Users != null)
                    {
                        foreach (var user in us.Users)
                        {
                            ListBox.Items.Add(user.Name);
                        }
                    }
                    break;
                case ComandsLib.GetFiveBestBooks:
                    var booksFive = JsonConvert.DeserializeObject<GetBookResponse>(jsonToReceive);
                    if (booksFive != null && booksFive.Books != null)
                    {
                        foreach (var book in booksFive.Books)
                        {
                            ListBox.Items.Add(book.Name);
                        }
                    }
                    break;
                case ComandsLib.Successful:
                    LoadigTmp();

                    break;
                case ComandsLib.ERROR:
                    var ERROR = JsonConvert.DeserializeObject<ClientRequest>(jsonToReceive);
                    if (ERROR != null)
                    {
                        ListBox.Items.Add($"Error: {ERROR.Message}");
                    }
                    break;
                default:
                    break;
            }
        }

        //_client.Close();
    }
    private void LoadigTmp()
    {
        var us = _cache.Get<UsersResponse>(ComandsLib.GetAllUsers.ToString());
        var bk = _cache.Get<GetBookResponse>(ComandsLib.GetAllBooks.ToString());

        if (us != null && bk != null)
        {
            foreach (var user in us.Users)
            {
                ListBox2.Items.Add(user.Name);
            }
        }
        else
        {
            var res = LoadingJsnFile();

            foreach (var b in res.Users)
            {
                ListBox2.Items.Add(b.Name);
            }
        }

    }

    private UsersResponse LoadingJsnFile()
    {
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        if (File.Exists("TmpBooks.json"))
        {
            using (var sr = File.OpenText("TmpBooks.json"!))
            {
                return (UsersResponse)js.Deserialize(sr, typeof(UsersResponse))!;
                //return sr.ReadToEnd();
            }

        }
        throw new NotImplementedException();
    }


    ~MainWindow()
    {

    }
    private byte[] Read(NetworkStream _networkStream)
    {
        _networkStream.Read(_buffer, 0, _buffer.Length);
        var resSize = BitConverter.ToInt32(_buffer, 0);

        var requestToReceive = new byte[resSize];
        _networkStream.Read(requestToReceive, 0, requestToReceive.Length);
        return requestToReceive;
    }

    private void Write(NetworkStream _networkStream)
    {
        var response = new ClientRequest
        {
            /*Command = ComandsLib.GetAllBooks,*/
            Command = ComandsLib.GetAllUsers,
            //Message = "Hi server, how are you ?"
            Message = $"{TimesTamp.GetTimesTamp()}"
        };
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        _buffer = BitConverter.GetBytes(responseBytes.Length);

        _networkStream.Write(_buffer, 0, _buffer.Length);
        _networkStream.Write(responseBytes, 0, responseBytes.Length);
    }

    private void SaveInJson(RequestResponseBase bk)
    {
        var js = new JsonSerializer();
        js.Formatting = Formatting.Indented;
        using (var sw = File.CreateText("TmpBooks.json"))
        {
            js.Serialize(sw, bk);
        }
    }

    private async void Get_Btn_Click(object sender, RoutedEventArgs e)
    {
        var requestToReceive = await _clientCore.SendRequestAsync("test");
        string jsonToReceive = Encoding.UTF8.GetString(requestToReceive);
        var comand = JsonConvert.DeserializeObject<RequestResponseBase>(jsonToReceive);

    }

    private void Btn_Connect_Click(object sender, RoutedEventArgs e)
    {
        var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
        TcpClient client = new TcpClient();
        client.Connect(endPoint);

        var networkStream = client.GetStream();
        var response = new ClientRequest
        {
            /*Command = ComandsLib.GetAllBooks,*/
            Command = ComandsLib.GetAllBooks,
            Message = "Hi server, how are you ?"
        };
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        _buffer = BitConverter.GetBytes(responseBytes.Length);

        networkStream.Write(_buffer, 0, _buffer.Length);
        networkStream.Write(responseBytes, 0, responseBytes.Length);

        //get info
        networkStream.Read(_buffer, 0, _buffer.Length);
        var resSize = BitConverter.ToInt32(_buffer, 0);

        var requestToReceive = new byte[resSize];
        networkStream.Read(requestToReceive, 0, requestToReceive.Length);

        string jsonToReceive = Encoding.UTF8.GetString(requestToReceive);
        var comand = JsonConvert.DeserializeObject<RequestResponseBase>(jsonToReceive);

        client.Close();
    }
}

/*                    if (books!=null && books.Books != null)
                    {
                        foreach (var book in books.Books)
                        {
                            ListBox.Items.Add(book.Name);
                        }
                    }*/

/*        if(books as GetBookResponse )
        {

        }*/
/*if(books != null && books.Books != null && books.Books.Count > 0)
{
    foreach (var book in books.Books)
    {
        ListBox.Items.Add($"Name=>{book.Name}, id=>{book.Id}");

        BitmapImage image = new BitmapImage();
        using (MemoryStream memoryStream = new MemoryStream(books.Books[0].Image))
        {
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = memoryStream;
            image.EndInit();
        }
        Image1.Source = image;

    }
}*/
// Send info get info variant 1
/*        var buff = new byte[4];
        string? message = "Book";
        var requestMessage = Encoding.UTF8.GetBytes(message);
        buff = BitConverter.GetBytes(requestMessage.Length);
        await networkStream.WriteAsync(buff, 0, buff.Length);
        await networkStream.WriteAsync(requestMessage, 0, requestMessage.Length);

        await networkStream.ReadAsync(buff, 0, buff.Length);
        var resSize = BitConverter.ToInt32(buff, 0);

        var requestToReceive = new byte[resSize];
        await networkStream.ReadAsync(requestToReceive, 0, requestToReceive.Length);

        string jsonToReceive = Encoding.UTF8.GetString(requestToReceive);

        List<Book> books = JsonConvert.DeserializeObject<List<Book>>(jsonToReceive);*/


/*foreach (var item in books)
{
    MessageBox.Show($"Name = {item.Name}, Price = {item.PriceForSale}");

}*/