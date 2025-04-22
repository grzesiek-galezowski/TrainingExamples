using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult;

public class ExistingPosts : IExistingPosts
{
  public async Task<string> AddAsync(PostDto postDto)
  {
    throw new System.NotImplementedException();
  }

  public async Task<IExistingPost> RetrieveByAsync(string id)
  {
    throw new System.NotImplementedException();
  }
}