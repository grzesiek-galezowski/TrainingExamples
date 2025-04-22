using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Link;

public interface ILinkingInProgress
{
  Task FailedFor(string id1, string id2, Exception exception);
}