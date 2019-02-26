using System;

namespace PloehKata
{
  public class UserInfrastructure : IUserInfrastructure
  {
    private IUserLookup _lookup;
    private IConnectorDestination _destination;

    public UserInfrastructure(IUserLookup lookup, IConnectorDestination destination)
    {
        _lookup = lookup;
        _destination = destination;
    }

    public IUserCommand CreateConnectionCommand(
      IConnectionInProgress connectionInProgress, string user1Id, string user2Id)
    {
      return new ConnectionCommand(connectionInProgress, user1Id, user2Id, _lookup, _destination);
    }

    public IConnectionInProgress CreateConnectionInProgress()
    {
      return new ConnectionInProgress();
    }
  }
}