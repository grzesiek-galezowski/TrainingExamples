using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Link;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

public interface IExistingPost
{
  void AssertIsNotTheSameAs(IExistingPost linkedPost);
  void Link(IExistingPost linkedPost, LinkPostsCommand command, IFollowers followers);
  void UpdateInAsync(ExistingPosts existingPosts);
}