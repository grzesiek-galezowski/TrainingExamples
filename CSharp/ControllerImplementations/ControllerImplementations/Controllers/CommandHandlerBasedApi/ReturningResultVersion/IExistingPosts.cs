using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Add;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

public interface IExistingPosts
{
  Task<PostCreatedDto> AddAsync(AddPostCommand command);
}