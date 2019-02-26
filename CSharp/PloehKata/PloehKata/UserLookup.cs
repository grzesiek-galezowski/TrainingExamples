using Functional.Maybe;

namespace PloehKata
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
        return _persistence.ReadById<UserDto>("Users", connectorId).Select(dto => (IConnector)new Connector(dto))
            .OrElse(() => new NoConnector());
    }

    public IConnectee LookupConnectee(string connecteeId)
    {
        return _persistence.ReadById<UserDto>("Users", connecteeId)
            .Select(dto => (IConnectee) new Connectee(dto))
            .OrElse(() => new NoConnectee());

    }
  }
}