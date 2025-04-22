using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;

public interface IActionResultBasedAddingInProgress : IAddingInProgress
{
  IActionResult Result();
}