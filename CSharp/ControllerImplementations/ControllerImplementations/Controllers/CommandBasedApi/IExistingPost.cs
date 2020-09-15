namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface IExistingPost
    {
        void AssertIsNotTheSameAs(IExistingPost linkedPost);
        void Link(IExistingPost linkedPost, ILinkingInProgress linkingInProgress, IFollowers followers);
        void UpdateInAsync(IExistingPosts existingPosts);
    }
}