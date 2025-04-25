using ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Add;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public class Followers : IFollowers
{
  public async Task Notify(AddPostCommand command)
  {
    throw new System.NotImplementedException();
  }
}