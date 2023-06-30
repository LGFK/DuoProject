using ComandLibrary;
using CommunicationLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore.Model;
public static class DataBase
{
    public static RequestResponseBase Create(RequestResponseBase request) =>
        request is not null ? request : new RequestResponseBase() { Command = ComandsLib.ERROR };
}
