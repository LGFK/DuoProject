using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary;
public class Genre
{
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;
    public Book? Book { get; set; }
}
