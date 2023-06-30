using System;
using System.Runtime.Serialization;

namespace ClientTest.TestError;
[Serializable]
internal class OtherErrorsDuringConnection : Exception
{
    public OtherErrorsDuringConnection()
        : base($"Other errors that may occur during connection ")
    {
    }
}