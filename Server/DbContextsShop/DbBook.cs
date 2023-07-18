using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using Server.Context;

namespace Server.DbContextsShop;
public class DbBook
{
    private BookShopDbContext _dbContext;
    public DbBook(DbContextOptions options)
    {
        _dbContext = new BookShopDbContext((DbContextOptions<BookShopDbContext>)options);
    }

    ~DbBook()=>
        _dbContext.Dispose();
    
    public List<Book> GetAllBooks() =>
         _dbContext.Books
            .Include(b => b.Publisher)
            .Include(b => b.Genre)
            .Include(b => b.Author)
            .Include(b =>b.CountBooks)
            .ToList();

    public List<Book> GetMaxPriceBooks() =>
        _dbContext.Books
        .OrderByDescending(b => b.Cost)
        .ToList();

    public List<Book> GetTopFiveGenre(string? genre)=>
        _dbContext.Books
            .Include(b => b.Genre)
            .Include(b => b.Publisher)
            .Include(b => b.CountBooks)
            .Where(b => b.Genre.Name == genre)
            .OrderByDescending(b => b.Cost)
            .Take(5)
            .ToList();
    
    public void AddNewBook(Book book)
    {
        var isCheck = _dbContext.Books.FirstOrDefault(b => b.Id == book.Id);
        if (isCheck != null)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
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
    public void EditBoks(int id, Book book)
    {
        var bk = _dbContext.Books.Find(id);
        if (bk != null)
        {
            bk.Name = book.Name;
            bk.NumberOfPages = book.NumberOfPages;
            bk.Cost = book.Cost;
            bk.PriceForSale = book.PriceForSale;
            bk.Image = book.Image;
            bk.CountBooks = new CountBooks() { Count = book.CountBooks.Count };
            bk.Publisher = new Publisher() { Name = book.Publisher.Name };
            bk.Genre = new Genre() { Name = book.Genre.Name };
            _dbContext.SaveChanges();
        }
    }
}
/*Id = b.Id,
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
                },*/