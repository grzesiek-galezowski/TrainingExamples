using PloehKata.Ports;

namespace PloehKata.Logic
{
    public interface IExistingConnector
    {
      void AddConnection(UserDto connecteeDto);
    }
}