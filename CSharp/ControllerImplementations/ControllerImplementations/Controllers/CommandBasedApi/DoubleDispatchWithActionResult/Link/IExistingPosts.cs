using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

public interface IExistingPosts
{
  Task<string> Add(PostDto postDto);
  Task<IExistingPost> RetrieveBy(string id);
}