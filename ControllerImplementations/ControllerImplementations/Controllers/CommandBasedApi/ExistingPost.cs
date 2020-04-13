namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public class ExistingPost : IExistingPost
    {
        public void AssertIsNotTheSameAs(IExistingPost linkedPost)
        {
            throw new System.NotImplementedException();
        }

        public void Link(IExistingPost linkedPost, ILinkingInProgress linkingInProgress, IFollowers followers)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateInAsync(IExistingPosts existingPosts)
        {
            throw new System.NotImplementedException();
        }
    }
}