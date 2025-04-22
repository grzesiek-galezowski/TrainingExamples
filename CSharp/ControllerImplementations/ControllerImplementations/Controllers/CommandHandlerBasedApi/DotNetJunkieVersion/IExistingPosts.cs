using ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Add;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public interface IExistingPosts
{
  Task<PostCreatedDto> AddAsync(AddPostCommand command);
}