namespace ModelsLibrary;
public class User : UserDto
{
    public bool? IsAdmin { get; set; }
    public bool? IsActive { get; set; }
    public DateTime RegisterTime { get; set; }
    public List<Book> Books { get; set; } = new();
}
