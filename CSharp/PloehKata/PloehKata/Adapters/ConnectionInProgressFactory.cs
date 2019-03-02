namespace PloehKata.Adapters
{
  public class ConnectionInProgressFactory : IConnectionInProgressFactory
  {
    public IActionResultBasedConnectionInProgress CreateConnectionInProgress() //bug split the interface
    {
      return new JsonBasedConnectionInProgress();
    }
  }
}