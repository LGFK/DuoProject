using ComandLibrary;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CommunicationLibrary;
public class RequestResponseBase
{
    [Required]
    public ComandsLib Command { get; set; }
    public string? TimesTamp { get; set; }
}
