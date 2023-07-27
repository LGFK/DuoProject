using ModelsLibrary;
using Server.Context;
using Server.core;
using Server.Helper;

namespace Server;

public partial class Form1 : Form
{
    ServerCore _server;
    public Form1()
    {
        InitializeComponent();
        _server = new ServerCore();
        //itin default books

        using (var db = new BookShopDbContext(DbOptions.GetOptions()))
        {
            if (db.Database.EnsureCreated())
            {
                var author1 = new Author { Name = "Àâòîð 1" };
                var author2 = new Author { Name = "Àâòîð 2" };
                var genre1 = new Genre { Name = "Æàíð 1" };
                var genre2 = new Genre { Name = "Æàíð 2" };
                var publisher1 = new Publisher { Name = "Ïàáë³øåð1" };
                var publisher2 = new Publisher { Name = "Ïàáë³øåð2" };
                //default create
                db.Books.AddRange(
                new Book
                {
                    Name = "Book1",
                    Cost = 1,
                    Genre = genre1,
                    Author = author1,
                    NumberOfPages = 1,
                    PriceForSale = 1,
                    Publisher = publisher1,
                    TimeOfPublication = DateTime.Now,
                    CountBooks = new CountBooks() { Count = 100 }
                },

                new Book
                {
                    Name = "Book2",
                    Cost = 13,
                    Genre = genre1,
                    Author = author1,
                    NumberOfPages = 1,
                    PriceForSale = 1,
                    Publisher = publisher1,
                    TimeOfPublication = DateTime.Now,
                    CountBooks = new CountBooks() { Count = 6 }
                },

                new Book
                {
                    Name = "Book3",
                    Cost = 11,
                    Genre = genre2,
                    Author = author2,
                    NumberOfPages = 123,
                    PriceForSale = 11,
                    Publisher = publisher2,
                    TimeOfPublication = DateTime.Now,
                    CountBooks = new CountBooks() { Count = 4 }
                },

                new Book
                {
                    Name = "Book4",
                    Cost = 12,
                    Genre = genre2,
                    Author = author2,
                    NumberOfPages = 1234,
                    PriceForSale = 134,
                    Publisher = publisher2,
                    TimeOfPublication = DateTime.Now,
                    CountBooks = new CountBooks() { Count = 12 }
                },

                new Book
                {
                    Name = "Book5",
                    Cost = 14,
                    Genre = genre2,
                    Author = author2,
                    NumberOfPages = 1244,
                    PriceForSale = 1424,
                    Publisher = publisher1,
                    TimeOfPublication = DateTime.Now,
                    CountBooks = new CountBooks() { Count = 2 }
                },

                new Book
                {
                    Name = "Book6",
                    Cost = 15,
                    Genre = genre2,
                    Author = author1,
                    NumberOfPages = 241,
                    PriceForSale = 451,
                    Publisher = publisher1,
                    TimeOfPublication = DateTime.Now,
                    CountBooks = new CountBooks() { Count = 45 }
                }
                );
                db.SaveChanges();
            }

        }
    }

    private void button1_Click(object sender, EventArgs e)
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