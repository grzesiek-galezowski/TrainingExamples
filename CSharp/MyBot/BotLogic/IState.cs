using System.Threading.Tasks;

namespace BotLogic
{
    public interface IState
    {
        Task OnWatchGameCatalogAsync(IUser user, DialogStateMachine dialogContext);
        Task OnEnterAsync(IUser user);
        Task OnGoShoppingAsync(IUser user, DialogStateMachine dialogStateMachine);
        Task OnYesAsync(IUser user, DialogStateMachine dialogStateMachine);
        Task OnNoAsync(IUser user, DialogStateMachine dialogStateMachine);
    }
}