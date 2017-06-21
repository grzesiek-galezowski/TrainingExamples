using System;

namespace DealingWithNull.Exceptions
{
  class ConnectionFactory
  {
    public Connection OpenConnectionTo(string uriString)
    {
      Uri uriResult;
      if (!Uri.TryCreate(uriString, UriKind.Absolute, out uriResult))
      {
        return null; //bad uri
      }

      var connection = new HttpConnection();

      if (connection.Open())
      {
        return connection;
      }
      else
      {
        return null;
      }
    }
  }
}