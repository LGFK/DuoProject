namespace ClientCore.Errors;

public class Error
{
    public Error(int errorCode, string errorType)
    {
        ErrorCode = errorCode;
        ErrorType = errorType;
    }
    public int ErrorCode { get; }
    public string ErrorType { get; }

    public static Error None { get; } = new Error(0, "None");
    public static Error NullValue { get; } = new Error(1, "NullValue");
    public static Error InvalidInput { get; } = new(2, "InvalidInput");
    public static Error ConnectionTimeout { get; } = new(3, "ConnectionTimeoute");
}

/*public class Error
{
    protected internal Error(int errorCOde, string errorType)
    {
        ErrorCode = errorCOde;
        ErrorType = errorType;
    }

    public int ErrorCode { get; }
    public string ErrorType { get; }
    public static Error None() =>
        new(0, "None");
    public static Error NullValue() =>
        new(1, "NullValue");
    public static Error InvalidInput() =>
        new(2, "InvalidInput");
    public static Error ConnectionTipeout() =>
        new(3, "ConnectionTimeout");
}*/
