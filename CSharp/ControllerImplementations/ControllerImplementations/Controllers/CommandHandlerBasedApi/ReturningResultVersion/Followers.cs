using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Add;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

public class Followers : IFollowers
{
  public async Task NotifyAsync(AddPostCommand command)
  {
    throw new System.NotImplementedException();
  }
}