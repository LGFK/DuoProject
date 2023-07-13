using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Context;

namespace Server.Helper;
public struct DbOptions
{
    private const string _path = "./config/appsetings.json";
    private const string _connectionName = "MainConnectionString";
    static public DbContextOptions<BookShopDbContext> GetOptions() =>
             new DbContextOptionsBuilder<BookShopDbContext>()
            .UseSqlServer(new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(_path)
            .Build()
            .GetConnectionString(_connectionName))
            .Options;
}


/*var config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile(_path)
                   .Build();

var options = new DbContextOptionsBuilder<BookShopDbContext>()
                    .UseSqlServer(config.GetConnectionString(_connectionName))
                    .Options;
        return options;*/