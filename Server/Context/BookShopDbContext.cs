using Microsoft.EntityFrameworkCore;
using ModelsLibrary;

namespace Server.Context;

public class BookShopDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Publisher> Publisher { get; set; }
    public DbSet<CountBooks> CountBooks { get; set; }
    public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options)
    {

    }
}
