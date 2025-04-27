using PloehKata.Ports;

namespace PloehKata.Logic
{
    public class UserDestination : IConnectorDestination
    {
      private readonly IPersistence _persistence;

      public UserDestination(IPersistence persistence)
      {
        _persistence = persistence;
      }

      public void Save(UserDto userDto)
        {
            _persistence.Save("Users", userDto);
        }
    }
}