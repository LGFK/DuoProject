using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Context;

namespace Server.Helper;
public struct DbOptions
{
    static public DbContextOptions<BookShopDbContext> GetOptions()
    {
        var config = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("./config/appsetings.json")
                           .Build();

        var options = new DbContextOptionsBuilder<BookShopDbContext>()
                            .UseSqlServer(config.GetConnectionString("MainConnectionString"))
                            .Options;
        return options;
    }
}
