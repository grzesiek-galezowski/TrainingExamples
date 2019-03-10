using System.Threading.Tasks;

namespace BotLogic.States
{
    public interface IState
    {
        Task OnWatchGameCatalogAsync(IUser user, IDialogContext dialogContext);
        Task OnEnterAsync(IUser user);
        Task OnGoShoppingAsync(IUser user, IDialogContext dialogStateMachine);
        Task OnYesAsync(IUser user, IDialogContext dialogStateMachine);
        Task OnNoAsync(IUser user, IDialogContext dialogStateMachine);
    }
}