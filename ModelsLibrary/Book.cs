using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary;
public class Book
{
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;


    public int GenreId { get;set; }
    public Genre? Genre { get; set; }

    public int PublisherId { get; set; }
    public Publisher? Publisher { get; set; }

    public CountBooks? CountBooks { get; set; }

    public int NumberOfPages { get; set; }
    public DateTime TimeOfPublication { get; set; }
    public int Cost { get; set; }
    public int PriceForSale { get; set; }
    public byte[]? Image { get; set; }
    
}
/*    [Column(TypeName = "nvarchar(50)")]
    public string NameAuthor { get; set; } = null!;
    [Column(TypeName = "nvarchar(50)")]
    public string Publisher { get; set; } = null!;*/
/*    [Column(TypeName = "nvarchar(50)")]
    public string Genre { get; set; } = null!;*/