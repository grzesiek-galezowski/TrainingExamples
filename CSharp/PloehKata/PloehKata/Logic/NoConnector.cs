using PloehKata.Ports;

namespace PloehKata.Logic
{
  public class NoConnector : IConnector
  {
    public void AttemptConnectionWith(IConnectee connectee, IConnectionInProgress connectionInProgress)
    {
      connectionInProgress.UserNotFound();
    }

    public void WriteTo(IConnectorDestination destination)
    {
      
    }
  }
}