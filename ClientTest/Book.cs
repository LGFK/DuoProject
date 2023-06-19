using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest;
public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NameAuthor { get; set; } = null!;
    public string Publisher { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int NumberOfPages { get; set; }
    public DateTime TimeOfPublication { get; set; }
    public int Cost { get; set; }
    public int PriceForSale { get; set; }
    public byte[] Image { get; set; }
    public List<User> Users { get; set; } = new();
}

