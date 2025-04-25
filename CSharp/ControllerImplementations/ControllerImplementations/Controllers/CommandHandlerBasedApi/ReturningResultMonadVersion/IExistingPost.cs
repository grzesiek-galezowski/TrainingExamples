using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Link;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion;

public interface IExistingPost
{
  void AssertIsNotTheSameAs(IExistingPost linkedPost);
  void Link(IExistingPost linkedPost, LinkPostsCommand command, IFollowers followers);
  void UpdateInAsync(ExistingPosts existingPosts);
}