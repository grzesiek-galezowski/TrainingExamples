using System.Collections.Generic;
using System.Threading;
using ApplicationLogic.Ports;

namespace Bootstrap.Controllers
{
  public class UsersRepository : IUsersRepository
  {
    private readonly Dictionary<string, UserDto> _usersByGuid = new Dictionary<string, UserDto>();

    public static IUsersRepository CreateUsersRepository()
    {
      return new UsersRepository();
    }

    public UserDto GetUserBy(string id)
    {
      Thread.Sleep(100);
      return _usersByGuid[id];
    }

    public void Add(UserDto userDto)
    {
      Thread.Sleep(100);
      _usersByGuid[userDto.Login] = userDto;
    }
  }
}