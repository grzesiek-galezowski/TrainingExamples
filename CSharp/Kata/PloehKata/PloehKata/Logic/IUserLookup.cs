namespace PloehKata.Logic
{
  public interface IUserLookup
  {
    IConnector LookupConnector(string connectorId);
    IConnectee LookupConnectee(string connecteeId);
  }
}