using ModelsLibrary;
using Newtonsoft.Json;
using Server.Context;
using Server.core;
using Server.DbContextsShop;
using Server.Helper;
using Server.models;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

public partial class Form1 : Form
{
    ServerCore _server;
    public Form1()
    {
        InitializeComponent();
        _server= new ServerCore();
        //itin default books

        using (var db = new BookShopDbContext(DbOptions.GetOptions()))
        {
            //db.Database.EnsureDeleted();
            if (db.Database.EnsureCreated())
            {
                //default create
                db.Books.AddRange(
                new Book { Name = "Book1", Cost = 1,Genre = new Genre() { Name = "Genre" },Author = new Author() { Name = "Authorr1"}, NumberOfPages = 1, PriceForSale = 1, Publisher = new Publisher() { Name ="Publisher"}, TimeOfPublication = DateTime.Now }
                //new Book { Name = "Book2", NameAuthor = "Author2", Cost = 2, Genre = "Ganre2", NumberOfPages = 2, PriceForSale = 2, Publisher = "Publisher2", TimeOfPublication = DateTime.Now },
                //new Book { Name = "Book3", NameAuthor = "Author3", Cost = 3, Genre = "Ganre3", NumberOfPages = 3, PriceForSale = 3, Publisher = "Publisher3", TimeOfPublication = DateTime.Now },
                //new Book { Name = "Book4", NameAuthor = "Author4", Cost = 4, Genre = "Ganre4", NumberOfPages = 4, PriceForSale = 4, Publisher = "Publisher4", TimeOfPublication = DateTime.Now },
                //new Book { Name = "Book5", NameAuthor = "Author5", Cost = 5, Genre = "Ganre5", NumberOfPages = 5, PriceForSale = 5, Publisher = "Publisher5", TimeOfPublication = DateTime.Now }
                );
                db.SaveChanges();

                //db.Users.AddRange(
                //    new User { Name = "Name1", Email = "qwert@gmail.com", Password = "12345", RegisterTime = DateTime.Now, Books = new List<Book>() },
                //    new User { Name = "Name2", Email = "asdf@gmail.com", Password = "56789", RegisterTime = DateTime.Now, Books = new List<Book>() },
                //    new User { Name = "Name3", Email = "zxcvb@gmail.com", Password = "rffvv ", RegisterTime = DateTime.Now, Books = new List<Book>() },
                //    new User { Name = "Name4", Email = "yhjuk@gmail.com", Password = "dfghj", RegisterTime = DateTime.Now, Books = new List<Book>() },
                //    new User { Name = "Name5", Email = "ikm@gmail.com", Password = "olmju", RegisterTime = DateTime.Now, Books = new List<Book>() });
                //db.SaveChanges();
            }
        }

    }
    private void button1_Click_1(object sender, EventArgs e)
    {
        _server.StartServer();
    }

}

//how get Book/ how get image from List book /
//DbBook db = new DbBook(DbOptions.GetOptions());
//db.GetAllBooks().ForEach(book => { listBox1.Items.Add(book.Name); });
//var res = db.GetAllBooks().FirstOrDefault(b => b.Id == 1);
/*        List<Book> res = db.GetAllBooks();

        var stream = new MemoryStream(res[0].Image);
        pictureBox1.Image = Image.FromStream(stream);
        db.GetMaxPriceBooks().ForEach(b => { listBox1.Items.Add($"Name = {b.Name}, Cost = {b.Cost}"); });*/

/*TcpListener tcpListener = new TcpListener(IPAddress.Any, 1448);
        tcpListener.Start();
        var client = await tcpListener.AcceptTcpClientAsync();
        if (client.Connected)
        {
            var networkStream = client.GetStream();

            var jsonToSend = JsonConvert.SerializeObject(res);
            var responseToSend = Encoding.UTF8.GetBytes(jsonToSend);
            var buffer = BitConverter.GetBytes(responseToSend.Length);

            networkStream.Write(buffer, 0, buffer.Length);
            networkStream.Write(responseToSend, 0, responseToSend.Length);
        }*/

//using (var dbb = new BookShopDbContext(DbOptions.GetOptions()))//add book img
//{
//    var book = dbb.Books.FirstOrDefault(b=>b.Id == 1);
//    if (book != null)
//    {
//        Image img = Image.FromFile("D:\\Desktop\\3.png");
//        MemoryStream tmpStream = new MemoryStream();
//        img.Save(tmpStream,ImageFormat.Png);
//        book.Image = tmpStream.ToArray();
//        dbb.SaveChanges();
//    }
//}

/*        using (var db = new  BookShopDbContext(options))
        {
            var books = db.Books.ToList();
            books.ForEach(book => listBox1.Items.Add($"Name={book.Name}, Ganer = {book.Genre}"));
        }*/