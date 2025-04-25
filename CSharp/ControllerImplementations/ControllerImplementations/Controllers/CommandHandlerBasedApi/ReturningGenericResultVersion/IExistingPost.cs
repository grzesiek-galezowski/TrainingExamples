using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Link;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

public interface IExistingPost
{
  void AssertIsNotTheSameAs(IExistingPost linkedPost);
  void Link(IExistingPost linkedPost, LinkPostsCommand command, IFollowers followers);
  void UpdateInAsync(ExistingPosts existingPosts);
}