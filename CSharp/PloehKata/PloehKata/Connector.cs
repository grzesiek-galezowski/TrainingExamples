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

    public void WriteTo(IConnectorDestination destination)
    {
      destination.Save(_userDto);
    }

    public void AddConnectionId(string id)
    {
        _userDto.Connections.Add(id);
    }
  }
}