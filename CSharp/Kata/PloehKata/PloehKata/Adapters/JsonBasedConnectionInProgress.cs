using Functional.Maybe;
using Microsoft.AspNetCore.Mvc;
using PloehKata.Ports;

namespace PloehKata.Adapters
{
  public class JsonBasedConnectionInProgress : IActionResultBasedConnectionInProgress
  {
    private Maybe<IActionResult> _response;

    public IActionResult ToActionResult()
    {
      return _response.OrElse(() => new NoResultException());
    }

    public void UserNotFound()
    {
      _response = BadRequest("User not found.");
    }

    public void OtherUserNotFound()
    {
      _response = BadRequest("Other user not found.");
    }

    public void InvalidUserId()
    {
      _response = BadRequest("Invalid user ID.");
    }

    public void InvalidOtherUserId()
    {
      _response = BadRequest("Invalid other user ID.");
    }

    public void Success(UserDto userDto)
    {
      _response = new JsonResult(userDto).ToMaybe<IActionResult>();
    }

    private static Maybe<IActionResult> BadRequest(string str)
    {
      return new BadRequestObjectResult(str).ToMaybe<IActionResult>();
    }
  }
}