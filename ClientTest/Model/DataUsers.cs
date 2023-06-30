using ClientTest.Helpers;
using ComandLibrary;
using CommunicationLibrary;
using Newtonsoft.Json;

namespace ClientTest.Model;
public static class DataUsers
{
    public static RequestResponseBase Users(string? jsonToReceive)
    {
        var users = JsonConvert.DeserializeObject<UsersResponse>(jsonToReceive!);
        if (users is null)
        {
            return new RequestResponseBase() { Command = ComandsLib.ERROR };
        }

        TimesTamp.SaveTimesTamp(users.TimesTamp);
        return users;
    }
}
