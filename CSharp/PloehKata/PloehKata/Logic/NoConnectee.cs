using PloehKata.Ports;

namespace PloehKata.Logic
{
    public class NoConnectee : IConnectee
    {
      public void AttemptConnectionFrom(
          IExistingConnector connector, 
          IConnectionInProgress connectionInProgress)
        {
            connectionInProgress.OtherUserNotFound();
        }
    }
}