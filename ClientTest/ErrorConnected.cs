using System;

namespace ClientTest;

public sealed class ErrorConnected : Exception
{
	public ErrorConnected()
		: base($"Error connected or bed port connection ")
	{
	}

}