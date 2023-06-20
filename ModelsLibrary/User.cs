namespace ModelsLibrary;
public class User : UserDto
{
    public DateTime RegisterTime { get; set; }
    public List<Book> Books { get; set; } = new();
}
