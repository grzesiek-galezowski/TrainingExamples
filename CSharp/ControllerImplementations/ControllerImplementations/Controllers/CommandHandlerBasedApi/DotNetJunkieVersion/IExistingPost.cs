using ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Link;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public interface IExistingPost
{
  void AssertIsNotTheSameAs(IExistingPost linkedPost);
  void Link(IExistingPost linkedPost, LinkPostsCommand command, IFollowers followers);
  void UpdateIn(ExistingPosts existingPosts);
}