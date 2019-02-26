using Microsoft.AspNetCore.Mvc;

namespace PloehKata
{
  public class ConnectionInProgress : IConnectionInProgress
  {
    public IActionResult ToActionResult()
    {
      throw new System.NotImplementedException();
    }

    public void UserNotFound()
    {
      throw new System.NotImplementedException();
    }
  }
}