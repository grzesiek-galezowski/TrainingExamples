using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

public interface IActionResultBasedLinkingInProgress : ILinkingInProgress
{
  IActionResult Result();
}