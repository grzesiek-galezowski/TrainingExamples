using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Link;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext;

public class ExistingPosts : IExistingPosts
{
  public async Task<string> Add(PostDto postDto)
  {
    throw new System.NotImplementedException();
  }

  public async Task<IExistingPost> RetrieveBy(string id)
  {
    throw new System.NotImplementedException();
  }
}