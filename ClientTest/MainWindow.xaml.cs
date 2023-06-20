using ComandLibrary;
using CommunicationLibrary;
using ModelsLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ClientTest;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private byte[] _buffer = new byte[4];
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
        TcpClient client = new TcpClient();
        client.Connect(endPoint);

        var networkStream = client.GetStream();
        var response = new ClientRequest
        {
            Command = ComandsLib.GetAllBooks,
            Message = "Hi server, how are you ?"
        };
        var jsonResponse = JsonConvert.SerializeObject(response);
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);
        _buffer = BitConverter.GetBytes(responseBytes.Length);

        networkStream.Write(_buffer,0, _buffer.Length);
        networkStream.Write(responseBytes,0, responseBytes.Length);

        //get info
        networkStream.Read(_buffer,0, _buffer.Length);
        var resSize = BitConverter.ToInt32(_buffer, 0);

        var requestToReceive = new byte[resSize];
        networkStream.Read(requestToReceive,0,requestToReceive.Length);

        string jsonToReceive = Encoding.UTF8.GetString(requestToReceive);
        var books = JsonConvert.DeserializeObject<GetBookResponse>(jsonToReceive);
        if(books != null && books.Books != null && books.Books.Count > 0)
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
        }
        client.Close();
    }
}


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