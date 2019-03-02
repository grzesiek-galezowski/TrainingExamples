using PloehKata.Ports;

namespace PloehKata.Logic
{
    public interface IConnectorDestination
    {
      void Save(UserDto userDto);
    }
}