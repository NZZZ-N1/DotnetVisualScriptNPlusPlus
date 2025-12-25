using System;

namespace DVSNPP_Demo.Exceptions;

public class NeventNotFoundException : Exception
{
	public NeventNotFoundException()
	{
		
	}
	public NeventNotFoundException(string message) : base(message)
	{
		
	}
}