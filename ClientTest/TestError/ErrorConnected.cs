using System;

namespace ClientTest.TestError;

public sealed class ErrorConnected : Exception
{
    public ErrorConnected()
        : base($"Error connected or bed port connection ")
    {
    }

}