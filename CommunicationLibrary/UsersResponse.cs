using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationLibrary;
public class UsersResponse : RequestResponseBase
{
    public List<User>? Users { get; set; }
}
