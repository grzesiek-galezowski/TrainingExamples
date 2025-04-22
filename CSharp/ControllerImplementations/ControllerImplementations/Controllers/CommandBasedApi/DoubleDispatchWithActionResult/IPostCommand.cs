using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult;

public interface IPostCommand
{
  Task ExecuteAsync();
}