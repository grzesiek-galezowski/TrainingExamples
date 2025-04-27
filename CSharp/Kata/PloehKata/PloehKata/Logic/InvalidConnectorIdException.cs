using System;

namespace PloehKata.Logic
{
    public class InvalidConnectorIdException : Exception
    {
      public InvalidConnectorIdException(string connectorId, Exception exception)
      : base($"Invalid connector id {connectorId}", exception)
      {
        
      }
    }
}