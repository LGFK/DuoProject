using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest;
public class User : UserDto
{
    public DateTime RegisterTime { get; set; }
    public List<Book> Books { get; set; } = new();

}
