using PloehKata.Ports;

namespace PloehKata.Logic
{
  public class UserUseCaseFactory : IUserUseCaseFactory
  {
    private readonly IUserLookup _userLookup;
    private readonly IConnectorDestination _connectorDestination;

    public UserUseCaseFactory(IConnectorDestination connectorDestination, IUserLookup userLookup)
    {
      _connectorDestination = connectorDestination;
      _userLookup = userLookup;
    }

    public IUserUseCase CreateConnectionUseCase(string user1Id, string user2Id, IConnectionInProgress connectionInProgress)
    {
      return new ConnectionUseCase(connectionInProgress, user1Id, user2Id, _userLookup, _connectorDestination);
    }
  }
}