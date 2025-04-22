using System;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;

public interface IAddingInProgress
{
  void SavedSuccessfully(PostDto postDto, string id);
  void FailedBecauseOf(Exception exception);
}