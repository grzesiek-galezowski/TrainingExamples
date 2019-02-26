namespace PloehKata
{
  public interface IUserRepository
  {
    IConnector LookupConnector(string connectorId);
    IConnectee LookupConnectee(string connecteeId);
  }
}