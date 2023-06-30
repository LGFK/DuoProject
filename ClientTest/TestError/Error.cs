namespace ClientTest.TestError;

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
    public static Error ConnectionTimeout { get; } = new(3, "ConnectionTimeout");


}