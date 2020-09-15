namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public class ResultInProgressFactory : IResultInProgressFactory
    {
        public IActionResultBasedAddingInProgress AddingInProgress()
        {
            return new AddingInProgress();
        }

        public IActionResultBasedLinkingInProgress LinkingInProgress()
        {
            return new LinkingInProgress();
        }
    }
}