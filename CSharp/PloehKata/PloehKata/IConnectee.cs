namespace PloehKata
{
  public interface IConnectee
  {
    void AttemptConnectionFrom(IExistingConnector connector, IConnectionInProgress connectionInProgress);
  }
}