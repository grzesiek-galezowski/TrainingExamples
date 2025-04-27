namespace PloehKata.Adapters
{
  public interface IConnectionInProgressFactory
  {
    IActionResultBasedConnectionInProgress CreateConnectionInProgress();
  }
}