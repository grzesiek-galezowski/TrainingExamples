using ApplicationLogic.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Bootstrap.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
      [HttpPost]
      public IActionResult CreateUser(UserDto user)
      {
          return null;
      }
  }
}
