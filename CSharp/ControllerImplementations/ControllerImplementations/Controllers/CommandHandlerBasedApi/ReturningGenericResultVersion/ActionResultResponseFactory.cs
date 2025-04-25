using System;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

public interface IResponseFactory<out TResponse>
{
  TResponse SavedSuccessfully(PostCreatedDto postCreatedDto);
  TResponse FailedBecauseOf(Exception exception);
  TResponse LinkedSuccessfully();
}

public class ActionResultResponseFactory : IResponseFactory<IActionResult>
{
  public IActionResult SavedSuccessfully(PostCreatedDto postCreatedDto)
  {
    return new OkObjectResult(postCreatedDto);
  }

  public IActionResult FailedBecauseOf(Exception e)
  {
    return new JsonResult(e)
    {
      StatusCode = 500
    };
  }

  public IActionResult LinkedSuccessfully()
  {
    return new OkObjectResult("Success");
  }
}