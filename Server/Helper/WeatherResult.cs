using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Helper;
internal class WeatherResult
{
    public bool Success { get; set; }
    public dynamic? Data { get; set; }
    public string? ErrorMessage { get; set; }
}
