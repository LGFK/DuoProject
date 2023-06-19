using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.models;
public class User : UserDto
{
    public DateTime RegisterTime { get; set; }
    public List<Book> Books { get; set; } = new();

}
