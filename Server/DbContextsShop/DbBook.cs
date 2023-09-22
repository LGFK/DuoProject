using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using Server.Context;

namespace Server.DbContextsShop;
public class DbBook
{
    private readonly BookShopDbContext _dbContext;
    public DbBook(DbContextOptions options)
    {
        _dbContext = new BookShopDbContext((DbContextOptions<BookShopDbContext>)options);
    }

    ~DbBook() =>
        _dbContext.Dispose();

    public List<Book> GetAllBooks() =>
         _dbContext.Books
            .Include(b => b.Publisher)
            .Include(b => b.Genre)
            .Include(b => b.Author)
            .Include(b => b.CountBooks)
            .ToList();

    public List<Book> GetMaxPriceBooks() =>
        _dbContext.Books
        .OrderByDescending(b => b.Cost)
        .ToList();

#pragma warning disable CS8602 
    public List<Book> GetTopFiveGenre(string? genre) =>
        _dbContext.Books
            .Include(b => b.Genre)
            .Include(b => b.Publisher)
            .Include(b => b.CountBooks)
            .Where(b => b.Genre.Name == genre)
            .OrderByDescending(b => b.Cost)
            .Take(5)
            .ToList();
#pragma warning restore CS8602 

    public async void AddNewBook(Book book)
    {
        var isCheck = _dbContext.Books.FirstOrDefault(b => b.Name == book.Name);
        if (isCheck is not null)
        {
            return;
        }
        var newBook = new Book() {Name = book.Name };

        var exGenre = _dbContext.Genres.FirstOrDefault(g => g.Name == book.Genre.Name);
        if (exGenre is null)
        {
            exGenre = new Genre { Name = book.Genre.Name};
            _dbContext.Genres.Add(exGenre);
            _dbContext.SaveChanges();
        }

        newBook.Genre = exGenre;
        newBook.GenreId = _dbContext.Genres
            .Where(g=>g.Name == newBook.Genre.Name)
            .Select(g=>g.Id)
            .FirstOrDefault();

        var exPublisher = _dbContext.Publisher
            .FirstOrDefault(b => b.Name == book.Publisher.Name);

        if (exPublisher is null)
        {
            exPublisher = new Publisher { Name = book.Genre.Name };
            _dbContext.Publisher.Add(exPublisher);
            _dbContext.SaveChanges();
        }
        newBook.Publisher = exPublisher;
        newBook.PublisherId = _dbContext.Publisher
            .Where(g=>g.Name == newBook.Publisher.Name)
            .Select(g=>g.Id)
            .FirstOrDefault();

        var exAuthor = _dbContext.Author
            .FirstOrDefault(b => b.Name == book.Author.Name);
        if (exAuthor is null) 
        {
            exAuthor = new Author { Name = book.Author.Name };
            _dbContext.Author.Add(exAuthor);
            _dbContext.SaveChanges();
        }
        newBook.Author = exAuthor;
        newBook.AuthorId = _dbContext.Author
            .Where(g => g.Name == newBook.Author.Name)
            .Select(g => g.Id)
            .FirstOrDefault();

        var exCountBooks = _dbContext.CountBooks
            .FirstOrDefault(b => b.BookId == book.Id);
        if (exCountBooks is null)
        {
            exCountBooks = new CountBooks { Count = 10,Book = newBook };
            _dbContext.CountBooks.Add(exCountBooks);
            //_dbContext.SaveChanges();
        }
        newBook.CountBooks = exCountBooks;
        newBook.Cost = book.Cost;


        _dbContext.Books.Add(newBook);
        _dbContext.SaveChanges();
    }
    public void RemoveBook(Book book)
    {
        var item = _dbContext.Books.Find(book.Id);
        if (item != null)
        {
            _dbContext.Books.Remove(item);
            _dbContext.SaveChanges();
        }
    }
    public void EditBoks(Book book)
    {
        var bk = _dbContext.Books
            .Include(b => b.Genre)
            .Include(b => b.Publisher)
            .Include(b => b.CountBooks)
            .FirstOrDefault(b => b.Id == book.Id);

        if (bk is null
            || !BookValidator(book))
        {
            return;
        }
#pragma warning disable CS8602
        bk.Name = book.Name;
        bk.NumberOfPages = book.NumberOfPages;
        bk.Cost = book.Cost;
        bk.PriceForSale = book.PriceForSale;
        bk.Image = book.Image;
        bk.CountBooks.Count = book.CountBooks.Count;

        var exGenre = _dbContext.Genres.FirstOrDefault(g => g.Name == book.Genre.Name);
        if (exGenre is null)
        {
            exGenre = new Genre { Name = book.Genre.Name };
            _dbContext.Genres.Add(exGenre);
        }
        bk.Genre = exGenre;

        var exAuthor = _dbContext.Author.FirstOrDefault(a => a.Name == book.Author.Name);
        if (exAuthor is null)
        {
            exAuthor = new Author { Name = book.Author.Name };
            _dbContext.Author.Add(exAuthor);
        }
        bk.Author = exAuthor;

        var exPublisher = _dbContext.Publisher.FirstOrDefault(p=>p.Name == book.Publisher.Name);
        if (exPublisher is null)
        {
            exPublisher = new Publisher { Name = book.Publisher.Name };
            _dbContext.Publisher.Add(exPublisher);
        }
        bk.Publisher = exPublisher;

        _dbContext.SaveChanges();
#pragma warning restore CS8602
    }

    private bool BookValidator(Book? book)
    {
        if (book is null)
        {
            return false;
        }

        if (book.Name is null)
        {
            return false;
        }

        if (book.Genre?.Name is null
            || book.Publisher?.Name is null
            || book.Author?.Name is null
            || book.CountBooks is null)
        {
            return false;
        }

        return true;
    }

}