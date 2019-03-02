using PloehKata.Ports;

namespace PloehKata.Logic
{
  public interface IConnectee
  {
    void AttemptConnectionFrom(IExistingConnector connector, IConnectionInProgress connectionInProgress);
  }
}