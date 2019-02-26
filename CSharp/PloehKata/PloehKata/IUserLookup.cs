namespace PloehKata
{
  public interface IUserLookup
  {
    IConnector LookupConnector(string connectorId);
    IConnectee LookupConnectee(string connecteeId);
  }
}