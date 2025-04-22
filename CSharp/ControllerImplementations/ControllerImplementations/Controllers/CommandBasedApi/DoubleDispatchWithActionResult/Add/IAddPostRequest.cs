using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;

public interface IAddPostRequest
{
  void AssertContentIsOfRequiredLength();
  void AssertContentContainsNoInappropriateWords();
  Task AddToAsync(IExistingPosts existingPosts, IAddingInProgress addingInProgress);
  Task NotifyAsync(IFollowers followers, IAddingInProgress addingInProgress);
}