using Microsoft.AspNetCore.Mvc;

namespace PloehKata
{
  public interface IConnectionInProgress
  {
    IActionResult ToActionResult();
    void UserNotFound();
  }
}