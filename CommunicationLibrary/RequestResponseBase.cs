using ComandLibrary;

namespace CommunicationLibrary;
public class RequestResponseBase
{
    public ComandsLib Command { get; set; }
    public string? TimesTamp { get; set; }
}
