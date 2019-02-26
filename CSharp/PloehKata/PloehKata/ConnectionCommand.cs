using System;

namespace PloehKata
{
  public class ConnectionCommand : IUserCommand
  {
    private readonly IConnectionInProgress _connectionInProgress;
    private readonly string _user1Id;
    private readonly string _user2Id;
    private readonly IUserLookup _lookup;
    private readonly IConnectorDestination _destination;

    public ConnectionCommand(IConnectionInProgress connectionInProgress,
        string user1Id,
        string user2Id,
        IUserLookup lookup, 
        IConnectorDestination destination)
    {
      _connectionInProgress = connectionInProgress;
      _user1Id = user1Id;
      _user2Id = user2Id;
      _lookup = lookup;
      _destination = destination;
    }

    public void Execute()
    {
        try
        {
            var connector = _lookup.LookupConnector(_user1Id);
            var connectee = _lookup.LookupConnectee(_user2Id);
            connector.AttemptConnectionWith(connectee, _connectionInProgress);
            connector.WriteTo(_destination);
        }
        catch (InvalidConnectorIdException e)
        {
            _connectionInProgress.InvalidUserId();
        }
        catch (InvalidConnecteeIdException e)
        {
            _connectionInProgress.InvalidOtherUserId();
        }
    }
  }
}