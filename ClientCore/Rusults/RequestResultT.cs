using ClientCore.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore.Rusults;
public class RequestResult<T> : RequestResult
{
    protected internal RequestResult(T value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    protected internal RequestResult(T value, bool isSuccess, Error[] errors)
    : base(isSuccess, errors)
    {
        Value = value;
    }
    public T Value { get; }

    public static RequestResult<T> Success(T value) =>
        value is not null ?
        Success<T>(value) :
        Failure<T>(Error.NullValue);
}
