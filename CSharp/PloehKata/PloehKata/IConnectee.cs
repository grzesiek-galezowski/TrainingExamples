namespace PloehKata
{
  public interface IConnectee
  {
    void AttemptConnectionFrom(Connector connector, IConnectionInProgress connectionInProgress);
  }
}