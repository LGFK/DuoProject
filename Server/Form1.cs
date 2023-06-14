using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.models;

namespace Server;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsetings.json")
            .Build();

        var options = new DbContextOptionsBuilder()
            .UseSqlServer(config.GetConnectionString("MainConnectionString"))
            .Options;

        using(var db = new BookShopBdContext(options))
        {

        }
    }
}
