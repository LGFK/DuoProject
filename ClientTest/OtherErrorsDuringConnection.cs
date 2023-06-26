using System;
using System.Runtime.Serialization;

namespace ClientTest;
[Serializable]
internal class OtherErrorsDuringConnection : Exception
{
    public OtherErrorsDuringConnection()
        : base($"Other errors that may occur during connection ")
    {
    }
}