namespace PloehKata
{
  public class UserRepository : IUserRepository
  {
    public IConnector LookupConnector(string connectorId)
    {
      throw new System.NotImplementedException();
    }

    public IConnectee LookupConnectee(string connecteeId)
    {
      throw new System.NotImplementedException();
    }
  }
}