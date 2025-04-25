using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

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