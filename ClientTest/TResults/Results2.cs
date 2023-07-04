using ClientTest.TestError;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest.TestResult;
public class Results2<T>
{
    protected internal Results2(bool isSuccess, Error error)
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
        Result = default;
    }

    protected internal Results2(bool isSuccess, Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        Result = default;
    }

    protected internal Results2(bool isSuccess, T result)
    {
        IsSuccess = isSuccess;
        Errors = new Error[] { };
        Result = result;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error[] Errors { get; }
    public T Result { get; }

    public static Results2<T> Success() =>
        new(true, default(T));

    public static Results2<T> Success(T value) =>
        new(true, value);

    public static Results2<T> Failure(Error error) =>
        new(false, error);

    public static Results2<T> Failure(Error[] errors) =>
        new(false, errors);

    public static Results2<T> Failure<TValue>(Error error) =>
        new(false, error);

    public static Results2<T> Failure<TValue>(Error[] errors) =>
        new(false, errors);

    public static Results2<T> Create(T value) =>
        value is not null ? Success(value) : Failure(Error.NullValue);

    public static Results2<T> Ensure(T value, Func<T, bool> predicate, Error error)
    {
        return predicate(value) ?
            Success(value) :
            Failure(error);
    }
}

