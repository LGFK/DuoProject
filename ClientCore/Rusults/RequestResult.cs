using ClientCore.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCore.Rusults;
public class RequestResult
{
    protected internal RequestResult(bool isSeccess,Error error)
    {
        if(isSeccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if(!isSeccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSucces = isSeccess;
        Errors = new[] { error };
    }

    protected internal RequestResult(bool isSeccess, Error[] errors)
    {
        IsSucces= isSeccess;
        Errors = errors;
    }

    public bool IsSucces { get; }
    public bool IsFailure => !IsSucces;
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

    public static RequestResult<TValue> Create<TValue>(TValue value)=>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}

public class RequestResult<T> : RequestResult
{
    protected internal RequestResult(T value, bool isSeccess, Error error) 
        : base(isSeccess, error)
    {
        Value = value;
    }
    public T Value { get; }
}