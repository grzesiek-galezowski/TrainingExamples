using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Add;

public interface IAddingInProgress
{
  Task SavedSuccessfully(PostDto postDto, string id);
  Task FailedBecauseOf(Exception exception);
}