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

    public void InvalidUserId()
    {
        throw new System.NotImplementedException();
    }

    public void InvalidOtherUserId()
    {
        throw new System.NotImplementedException();
    }

    public void Success(UserDto userDto)
    {
        throw new System.NotImplementedException();
    }
  }
}