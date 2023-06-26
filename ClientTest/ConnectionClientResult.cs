using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest;
public record class ConnectionClientResult
{
    private ConnectionClientResult(string? message)
    {
        Message = message;
    }

    public string? Message { get; init; }

    public static ConnectionClientResult NotConnection(string? message) =>
        new("Error conected to server");
    public static ConnectionClientResult OtherErrors(string? message) =>
        new("Other errors that may occur during connection");

    public static ConnectionClientResult Successful() =>
        new("The connected was successfully");
}
//Other errors that may occur during connection