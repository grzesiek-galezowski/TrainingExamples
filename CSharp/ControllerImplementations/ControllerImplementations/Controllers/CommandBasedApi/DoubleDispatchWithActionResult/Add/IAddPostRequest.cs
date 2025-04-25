using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;

public interface IAddPostRequest
{
  void AssertContentIsOfRequiredLength();
  void AssertContentContainsNoInappropriateWords();
  Task AddTo(IExistingPosts existingPosts, IAddingInProgress addingInProgress);
  Task Notify(IFollowers followers, IAddingInProgress addingInProgress);
}