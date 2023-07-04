using ClientTest.TestError;
using System;

namespace ClientTest.TestResult;
public class Result
{
    protected internal Result(bool isSuccess, Error error)
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
    }
    protected internal Result(bool isSuccess, Error[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error[] Errors { get; }
    public static Result Success() => new(true, Error.None);
    public static Result Success<TValue>(TValue value) =>
        new Result(true, Error.None);
    public static Result Failure(Error error) =>
        new(false, error);
    public static Result Failure(Error[] errors) =>
        new Result(false, errors);
    public static Result Failure<TValue>(Error error) =>
        new(false, error);
    public static Result Failure<TValue>(Error[] error) =>
    new(false, error);
    public static Result Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
    public static Result Ensure<T>(T value, Func<T, bool> predicate, Error error)
    {
        return predicate(value) ?
            Success() :
            Failure(error);
    }


}

