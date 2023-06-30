using ClientTest.TestError;
using CommunicationLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest.TestResult;
public class RequestAsyncResult
{
    protected internal RequestAsyncResult(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }
        IsSuccess = isSuccess;
        Errors = new[] { error };
        Value = null;
    }

    protected internal RequestAsyncResult(bool isSuccess, Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        Value = null;
    }

    protected internal RequestAsyncResult(bool isSuccess, Error error, object value)

    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        if (value == null)
        {
            throw new InvalidOperationException();
        }
        IsSuccess = isSuccess;
        Errors = new[] { error };
        Value = value;
    }
    public object? Value { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error[] Errors { get; }


    public static RequestAsyncResult Success() =>
        new(true, Error.None);
    public static RequestAsyncResult Success<TValue>(TValue value) =>
        new(true, Error.None, value);
    public static RequestAsyncResult Failure(Error error) =>
        new(false, error);
    public static RequestAsyncResult Failure(Error[] errors) =>
        new(false, errors);
    public static RequestAsyncResult Failure<TValue>(Error error) =>
        new(false, error);
    public static RequestAsyncResult Failure<TValue>(Error[] errors) =>
        new(false, errors);
    public static RequestAsyncResult Create<TValue>(TValue value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    public static RequestAsyncResult Ensure<T>(T value, Func<T, bool> predicate, Error error)
    {
        return predicate(value) ?
            Success() :
            Failure(error);
    }
}
