using ApplicationLogic.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
  public class ResultBuilder : IResultBuilder
  {
    private UserDto _userDto;

    public IActionResult BuildResult()
    {
      return new CreatedResult("api/users/" + _userDto.Login, _userDto.Login);
    }

    public void UserAddedSuccessfully(UserDto userDto)
    {
      _userDto = userDto;
    }
  }
}