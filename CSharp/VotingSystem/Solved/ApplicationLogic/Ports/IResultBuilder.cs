using System;

namespace ApplicationLogic.Ports
{
  public interface IResultBuilder
  {
    void UserAddedSuccessfully(UserDto userDto);
  }
}