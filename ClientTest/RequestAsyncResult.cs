using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest;
public class RequestAsyncResult
{
    public bool Success { get; set; }
    public byte[]? Data { get; set; }
    public string? ErrorMessage { get; set; }
}
