using CommunicationLibrary;
using Newtonsoft.Json;

namespace ClientCore.Model;
public static class DataUser
{
    public static RequestResponseBase Users(string? jsonToReceive)
    {
        var users = JsonConvert.DeserializeObject<RequestResponseBase>(jsonToReceive!);
        return DataBase.Create(users!);
    }

}
