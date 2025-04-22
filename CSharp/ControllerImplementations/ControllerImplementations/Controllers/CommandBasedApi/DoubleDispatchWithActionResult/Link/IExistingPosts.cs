using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

public interface IExistingPosts
{
  Task<string> AddAsync(PostDto postDto);
  Task<IExistingPost> RetrieveByAsync(string id);
}