using PloehKata.Ports;

namespace PloehKata.Logic
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

    public void AddConnection(UserDto anotherUserDto)
    {
        _userDto.Connections.Add(anotherUserDto);
        anotherUserDto.Connections.Add(_userDto);
    }
  }
}