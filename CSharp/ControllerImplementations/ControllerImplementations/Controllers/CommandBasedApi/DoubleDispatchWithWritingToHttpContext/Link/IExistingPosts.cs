using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Link;

public interface IExistingPosts
{
  Task<string> Add(PostDto postDto);
  Task<IExistingPost> RetrieveBy(string id);
}