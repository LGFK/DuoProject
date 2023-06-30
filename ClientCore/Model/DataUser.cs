using ComandLibrary;
using CommunicationLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore.Model;
public static class DataUser
{
    public static RequestResponseBase Users(string ? jsonToReceive)
    {
        var users = JsonConvert.DeserializeObject<RequestResponseBase>(jsonToReceive!);
        return DataBase.Create(users!);
    }

}
