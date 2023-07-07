using ClientCore.Errors;

namespace ClientCore.Rusults;
public static class ResultExtensions
{
    public static RequestResult<T> Ensure<T>(
        this RequestResult<T> result,
        Func<T , bool> predicate,
        Error error)
    {
        if(result.IsFailure)
        {
            return result;
        }

        if(predicate(result.Value))
        {
            return result;
        }

        return RequestResult.Failure<T>(error);
    }

    public static RequestResult<TOut> Map<TIn, TOut>(
        this RequestResult<TIn> result, 
        Func<TIn, TOut> mappingFunc)
    {
        return result.IsSuccess ?
            RequestResult.Success(mappingFunc(result.Value)) :
            RequestResult.Failure<TOut>(result.Errors);
    }

    public static RequestResult<T> Try<T>(
        Func<T> func,
        Error error)
    {
        try
        {
            return RequestResult.Success(func());
        }
        catch (Exception)
        {

            return RequestResult.Failure<T>(error);
        }
    }

    public static RequestResult<T> Recover<T>(
        this RequestResult<T> result,
        Func<RequestResult<T>> recoveryFunc)
    {
        return result.IsSuccess
            ? result
            : recoveryFunc();
    }

    public static RequestResult<TOut> Bind<Tin, TOut>(this RequestResult<Tin> result, Func<Tin, RequestResult<TOut>> func)
    {
        if (result.IsSuccess)
        {
            return func(result.Value);
        }

        return RequestResult.Failure<TOut>(result.Errors);
    }



}
