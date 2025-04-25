using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion;

public class Followers : IFollowers
{
  public async Task NotifyAsync(AddPostCommand command)
  {
    throw new System.NotImplementedException();
  }
}