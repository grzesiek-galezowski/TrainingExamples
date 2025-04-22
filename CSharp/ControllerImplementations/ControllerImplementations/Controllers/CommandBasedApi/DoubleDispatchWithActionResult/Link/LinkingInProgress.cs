using System;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

internal class LinkingInProgress : IActionResultBasedLinkingInProgress
{
  public void FailedFor(string id1, string id2, Exception exception)
  {
    throw new NotImplementedException();
  }

  public IActionResult Result()
  {
    throw new NotImplementedException();
  }
}