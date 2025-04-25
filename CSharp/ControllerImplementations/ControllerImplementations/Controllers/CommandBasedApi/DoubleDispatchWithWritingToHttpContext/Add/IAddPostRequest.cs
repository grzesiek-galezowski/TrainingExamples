using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Link;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Add;

public interface IAddPostRequest
{
  void AssertContentIsOfRequiredLength();
  void AssertContentContainsNoInappropriateWords();
  Task AddTo(IExistingPosts existingPosts, IAddingInProgress addingInProgress);
  Task Notify(IFollowers followers, IAddingInProgress addingInProgress);
}