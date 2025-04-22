using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Link;

internal class LinkingInProgress(ControllerBase controller) : ILinkingInProgress
{
  public async Task FailedFor(string id1, string id2, Exception exception)
  {
    await controller.Problem("something", "instance", 400, "title", "type")
      .ExecuteResultAsync(controller.ControllerContext);
  }
}