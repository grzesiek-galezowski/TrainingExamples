using ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Add;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public class ExistingPosts : IExistingPosts
{
  public async Task<PostCreatedDto> Add(AddPostCommand command)
  {
    throw new System.NotImplementedException();
  }

  public async Task<IExistingPost> RetrieveBy(string id1)
  {
    throw new System.NotImplementedException();
  }
}