using ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Add;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public class ExistingPosts : IExistingPosts
{
  public async Task<PostCreatedDto> AddAsync(AddPostCommand command)
  {
    throw new System.NotImplementedException();
  }

  public async Task<IExistingPost> RetrieveByAsync(string id1)
  {
    throw new System.NotImplementedException();
  }
}