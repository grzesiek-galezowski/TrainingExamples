using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Add;

public interface IAddingInProgress
{
  Task SavedSuccessfullyAsync(PostDto postDto, string id);
  Task FailedBecauseOf(Exception exception);
}