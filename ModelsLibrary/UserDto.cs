using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsLibrary;
public class UserDto
{
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;
    [Column(TypeName = "nvarchar(50)")]
    public string Email { get; set; } = null!;
    [Column(TypeName = "nvarchar(50)")]
    public string Password { get; set; } = null!;
}
