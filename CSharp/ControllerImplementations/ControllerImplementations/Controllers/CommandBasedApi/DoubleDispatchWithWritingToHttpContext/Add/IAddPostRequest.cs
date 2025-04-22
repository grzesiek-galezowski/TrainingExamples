using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Link;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Add;

public interface IAddPostRequest
{
  void AssertContentIsOfRequiredLength();
  void AssertContentContainsNoInappropriateWords();
  Task AddToAsync(IExistingPosts existingPosts, IAddingInProgress addingInProgress);
  Task NotifyAsync(IFollowers followers, IAddingInProgress addingInProgress);
}