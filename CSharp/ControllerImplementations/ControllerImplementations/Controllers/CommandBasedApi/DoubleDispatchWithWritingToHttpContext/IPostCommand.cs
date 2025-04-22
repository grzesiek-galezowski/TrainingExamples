using System.Threading;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext;

public interface IPostCommand
{
  Task Execute(CancellationToken token);
}