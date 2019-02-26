namespace PloehKata
{
  public class Connector : IConnector, IExistingConnector
  {
      private readonly UserDto _userDto;

      public Connector(UserDto userDto)
      {
          _userDto = userDto;
      }

    public void AttemptConnectionWith(IConnectee connectee, IConnectionInProgress connectionInProgress)
    {
      connectee.AttemptConnectionFrom(this, connectionInProgress);
    }

    public void WriteTo(IUserRepository repository)
    {
      throw new System.NotImplementedException();
    }

    public void ConnectWith(string id)
    {
        throw new System.NotImplementedException();
    }
  }
}