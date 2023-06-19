using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Server.Helper;

namespace Server.Context;
/// <summary>
/// Потрібен якщо ми використовуємо міграцію із конструктором позамовчуванням
/// </summary>
public class BookShopDbFactory : IDesignTimeDbContextFactory<BookShopDbContext>
{
    public BookShopDbContext CreateDbContext(string[] args)
    {
        return new BookShopDbContext(DbOptions.GetOptions());
    }
}
