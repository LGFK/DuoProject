using ComandLibrary;
using CommunicationLibrary;

namespace ClientCore.Model;
public static class DataBase
{
    public static RequestResponseBase Success(RequestResponseBase request) => request;
    public static RequestResponseBase Create(RequestResponseBase request) =>
        request is not null ? request : new RequestResponseBase() { Command = ComandsLib.ERROR };


}
/*public static RequestResult<TValue> Create<TValue>(TValue value) =>
    value is not null ? Success(value) : Failure<TValue>(Error.NullValue);*/
