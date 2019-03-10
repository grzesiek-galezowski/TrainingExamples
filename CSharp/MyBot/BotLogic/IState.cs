using System.Threading.Tasks;
using BotLogic;

namespace BotBuilderEchoBotV4.Logic
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