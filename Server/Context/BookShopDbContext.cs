using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelsLibrary;

namespace Server.Context;

public class BookShopDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options)
    {
    }
}

//[NotMapped] -- ignore table  якщо не хочемо створювати таблицю в базі 
// коли використовуєму міграцію не використовуємо /*Database.EnsureDeleted();Database.EnsureCreated();*/
//add-migration NAME_MIGRATION
//update-database

//коли ми використовуємо міграцюю він спочатку шукає конструктор по замовчуванню якщо він є він кикликає його, якщо для нашого класу його не має 
//він шукає спецальну фабрику яка вміє створювати обекти нашого контексту і викликає CreateDbContext 

/*public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options)// якщо використовуємо із міграцією то повинні реалізувати класс BookShopDbContextFactory Обовязково це 2 варіант 
    {
*//*        Database.EnsureDeleted(); // не використовувати разом із міграцією (буде конфлікт)
        Database.EnsureCreated();*//*
    }*/

/*    protected override void OnModelCreating(ModelBuilder modelBuilder) //як саме ми хочему створити базу 
    {
        //base.OnModelCreating(modelBuilder);
        //modelBuilder.Ignore<Books>(); // ignore table
        //modelBuilder.Entity<Books>().UseTpcMappingStrategy();
    }*/

/*    public BookShopDbContext()// варіант 1 коли використовуємо міграцію конструктор по замовчуванню і реалізуємо OnConfiguring
{

}
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)// варіант 1 коли використовуємо міграцію 
{
    var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsetings.json")
        .Build();
    optionsBuilder.UseSqlServer(config.GetConnectionString("MainConnectionString"));
}*/