namespace PloehKata
{
  public interface IConnector
  {
    void AttemptConnectionWith(IConnectee connectee, IConnectionInProgress connectionInProgress);
    void WriteTo(IConnectorDestination destination);
  }
}