using ClientCore.Errors;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore.Rusults;
public class RequestResult
{
    protected internal RequestResult(bool isSuccess,Error error)
    {
        if(isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if(!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Errors = new[] { error };
    }

    protected internal RequestResult(bool isSuccess, Error[] errors)
    {
        IsSuccess= isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error[] Errors { get; }

    public static RequestResult Success() =>
        new(true, Error.None);

    public static RequestResult<TValue> Success<TValue>(TValue value)=>
        new RequestResult<TValue>(value, true,Error.None);

    public static RequestResult Failure(Error error) =>
        new(false, error);

    public static RequestResult Failure(Error[] errors)=>
        new(false,errors);

    public static RequestResult<TValue> Failure<TValue>(Error error) =>
        new(default,false, error);

    public static RequestResult<TValue> Failure<TValue>(Error[] errors) =>
        new(default, false, errors);

    public static RequestResult<TValue> Create<TValue>(TValue value)=>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

/*    public static async Task<RequestResult<T>> From<T>(Func<Task<T>> operation)
    {
        try
        {
            var result = await operation();
            return RequestResult.Success(result);
        }
        catch (Exception ex)
        {
            return RequestResult.Failure<T>(Error.NullValue);
        }
    }*/
}