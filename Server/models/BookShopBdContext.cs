using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.models;
internal class BookShopBdContext: DbContext
{
    public DbSet<Books> Books { get; set; }

    public BookShopBdContext(DbContextOptions options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}
