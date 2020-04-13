namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface IResultInProgressFactory
    {
        IActionResultBasedAddingInProgress AddingInProgress();
        IActionResultBasedLinkingInProgress LinkingInProgress();
    }
}