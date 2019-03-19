using System;

namespace ApplicationLogic.Ports
{
  public interface IUsersRepository
  {
    UserDto GetUserBy(string id);
    void Add(UserDto userDto);
  }
}