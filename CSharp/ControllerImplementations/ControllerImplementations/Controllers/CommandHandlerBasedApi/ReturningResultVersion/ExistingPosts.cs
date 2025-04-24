using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Add;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

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