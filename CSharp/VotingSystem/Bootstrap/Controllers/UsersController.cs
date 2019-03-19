using ApplicationLogic;
using ApplicationLogic.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly ICommandFactory _commandFactory;

    public UsersController(ICommandFactory commandFactory)
    {
      _commandFactory = commandFactory;
    }

    [HttpPost]
    public IActionResult RegisterUser(UserDto userDto)
    {
      var resultBuilder = new ResultBuilder();
      _commandFactory.CreateRegisterUserCommand(userDto, resultBuilder).Execute();

      return resultBuilder.BuildResult();
    }

    [HttpGet("{login}")]
    public IActionResult GetUser(string login)
    {
      return new OkObjectResult(
        _commandFactory.CreateGetUserByIdQuery(login).Execute());
    }
  }
}
