using PloehKata.Ports;

namespace PloehKata.Logic
{
    public class Connectee : IConnectee
    {
      private readonly UserDto _userDto;

      public Connectee(UserDto userDto)
        {
            _userDto = userDto;
        }

      public void AttemptConnectionFrom(IExistingConnector connector, IConnectionInProgress connectionInProgress)
        {
            connector.AddConnection(_userDto);
            connectionInProgress.Success(_userDto);
        }
    }
}