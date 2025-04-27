using PloehKata.Logic;
using PloehKata.Ports;

namespace PloehKata.Bootstrap
{
  public class ApplicationLogicRoot
  {
    private readonly UserUseCaseFactory _userUseCaseFactory;

    public ApplicationLogicRoot(IPersistence persistence)
    {
      var noSqlPersistence = persistence;
      IUserLookup lookup = new UserLookup(noSqlPersistence);
      IConnectorDestination destination = new UserDestination(noSqlPersistence);
      _userUseCaseFactory = new UserUseCaseFactory(destination, lookup);
    }

    public IUserUseCaseFactory GetUserUseCaseFactory()
    {
      return _userUseCaseFactory;
    }
  }
}