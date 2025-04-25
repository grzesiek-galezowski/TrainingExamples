using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

public interface IFollowers
{
  Task NotifyAsync(AddPostCommand command);
}