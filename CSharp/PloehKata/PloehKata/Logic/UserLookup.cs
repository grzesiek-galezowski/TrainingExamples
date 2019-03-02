using System;
using Functional.Maybe;
using PloehKata.Ports;

namespace PloehKata.Logic
{
  public class UserLookup : IUserLookup
  {
    private readonly IPersistence _persistence;

    public UserLookup(IPersistence persistence)
    {
        _persistence = persistence;
    }

    public IConnector LookupConnector(string connectorId)
    {
      try
      {
        return _persistence.ReadById<UserDto>("Users", connectorId)
          .Select(dto => (IConnector) new Connector(dto))
          .OrElse(() => new NoConnector());
      }
      catch (Exception e)
      {
        throw new InvalidConnectorIdException(connectorId, e);
      }

      //bug exception
    }

    public IConnectee LookupConnectee(string connecteeId)
    {
      try
      {
        return _persistence.ReadById<UserDto>("Users", connecteeId)
          .Select(dto => (IConnectee) new Connectee(dto))
          .OrElse(() => new NoConnectee());
        //bug exception
      }
      catch (Exception e)
      {
        throw new InvalidConnecteeIdException(connecteeId, e);
      }

    }
  }
}