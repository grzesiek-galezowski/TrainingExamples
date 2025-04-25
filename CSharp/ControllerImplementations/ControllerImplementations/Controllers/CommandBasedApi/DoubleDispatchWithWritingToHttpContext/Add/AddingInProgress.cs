using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Add;

internal class AddingInProgress(ControllerBase controller) : IAddingInProgress
{
  public async Task SavedSuccessfully(PostDto postDto, string id)
  {
    await controller.Ok(postDto).ExecuteResultAsync(controller.ControllerContext);
  }

  public async Task FailedBecauseOf(Exception exception)
  {
    await new JsonResult(exception)
    {
      StatusCode = 500
    }.ExecuteResultAsync(controller.ControllerContext);
  }
}