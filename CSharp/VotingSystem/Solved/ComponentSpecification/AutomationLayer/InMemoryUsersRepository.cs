using System.Collections.Generic;
using ApplicationLogic.Ports;

namespace ComponentSpecification.AutomationLayer
{
  public class InMemoryUsersRepository : IUsersRepository
  {
    private readonly Dictionary<string, UserDto> _usersByLogin = new Dictionary<string, UserDto>();

    public UserDto GetUserBy(string id)
    {
      return _usersByLogin[id];
    }

    public void Add(UserDto userDto)
    {
      _usersByLogin[userDto.Login] = userDto;
    }
  }
}