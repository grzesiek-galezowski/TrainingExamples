namespace PloehKata
{
  public class Connector : IConnector
  {
    public void AttemptConnectionWith(IConnectee connectee, IConnectionInProgress connectionInProgress)
    {
      connectee.AttemptConnectionFrom(this, connectionInProgress);
    }

    public void WriteTo(IUserRepository repository)
    {
      throw new System.NotImplementedException();
    }
  }
}