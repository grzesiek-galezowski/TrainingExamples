using System;

namespace PloehKata.Logic
{
    public class InvalidConnecteeIdException : Exception
    {
      public InvalidConnecteeIdException(string connecteeId, Exception exception)
        : base($"Invalid connectee id {connecteeId}", exception)
      {
      
      }
    }
}