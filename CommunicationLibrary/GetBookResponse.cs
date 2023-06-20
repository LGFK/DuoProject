using ModelsLibrary;
namespace CommunicationLibrary;
public class GetBookResponse : RequestResponseBase
{
    public List<Book>? Books { get; set; }
}
