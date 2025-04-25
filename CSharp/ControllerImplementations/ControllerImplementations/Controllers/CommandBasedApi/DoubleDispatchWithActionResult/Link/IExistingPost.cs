namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

public interface IExistingPost
{
  void AssertIsNotTheSameAs(IExistingPost linkedPost);
  void Link(IExistingPost linkedPost, ILinkingInProgress linkingInProgress, IFollowers followers);
  void UpdateIn(IExistingPosts existingPosts);
}