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
        var isCheck = _dbContext.Books.FirstOrDefault(book);
        if (isCheck != null)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
    }
    public void RemoveBook(Book book)
    {
        var item = _dbContext.Books.FirstOrDefault(book);
        if (item != null)
        {
            _dbContext.Books.Remove(item);
            _dbContext.SaveChanges();
        }
    }
    public void EditBoks(int id, Book book)
    {
        var bk = _dbContext.Books.FirstOrDefault(b => b.Id == id);
        if (bk != null)
        {
            bk = book;
            _dbContext.SaveChanges();
        }
    }
}
