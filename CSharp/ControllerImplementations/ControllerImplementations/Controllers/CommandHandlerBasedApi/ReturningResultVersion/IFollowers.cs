using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Add;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

public interface IFollowers
{
  Task NotifyAsync(AddPostCommand command);
}