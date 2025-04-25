using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

public class Followers : IFollowers
{
  public async Task Notify(AddPostCommand command)
  {
    throw new System.NotImplementedException();
  }
}