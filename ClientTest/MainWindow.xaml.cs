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
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1448);
        TcpClient client = new TcpClient();
        client.Connect(endPoint);

        var networkStream = client.GetStream();
        var buff = new byte[4];
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

        List<Book> books = JsonConvert.DeserializeObject<List<Book>>(jsonToReceive);

        
        foreach (var item in books)
        {
            MessageBox.Show($"Name = {item.Name}, Price = {item.PriceForSale}");

        }
        BitmapImage image = new BitmapImage();
        using (MemoryStream memoryStream = new MemoryStream(books[0].Image))
        {
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = memoryStream;
            image.EndInit();
        }
        Image1.Source = image;
        client.Close();

    }
}
