using Microsoft.EntityFrameworkCore;
using Server.Context;
using Server.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbContextsShop;
public class DbBook
{
    private BookShopDbContext _dbContext;
    public DbBook(DbContextOptions options)
    {
        _dbContext = new BookShopDbContext((DbContextOptions<BookShopDbContext>)options);
    }
    ~DbBook() 
    {
        _dbContext.Dispose();
    }
    public List<Book> GetAllBooks()
    {
        return _dbContext.Books.ToList();
    }
    public List<Book> GetMaxPriceBooks()
    {
        return _dbContext.Books.OrderByDescending(b => b.Cost).ToList();
    }
    public List<Book> GetTopFiveGanre(string? ganre)
    {
        return _dbContext.Books.Where(b=>b.Genre == ganre).OrderByDescending(b => b.Cost).Take(5).ToList();
    }
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
    public void EditBoks(int id,Book book)
    {
        var bk = _dbContext.Books.FirstOrDefault(b => b.Id == id);
        if (bk != null)
        {
            bk = book;
            _dbContext.SaveChanges();
        }
    }
}
