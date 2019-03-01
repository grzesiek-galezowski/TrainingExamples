using Microsoft.AspNetCore.Mvc;

namespace PloehKata
{
  public interface IConnectionInProgress
  {
    IActionResult ToActionResult();
    void UserNotFound();
    void InvalidUserId();
    void InvalidOtherUserId();
    void Success(UserDto userDto);
  }
}