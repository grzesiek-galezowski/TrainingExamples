using System.Threading.Tasks;

namespace BotBuilderEchoBotV4.Navigation
{
    public interface IState
    {
        Task OnWatchGameCatalogAsync(User user, DialogStateMachine dialogContext);
        Task OnEnterAsync(User user);
        Task OnGoShoppingAsync(User user, DialogStateMachine dialogStateMachine);
        Task OnYesAsync(User user, DialogStateMachine dialogStateMachine);
        Task OnNoAsync(User user, DialogStateMachine dialogStateMachine);
    }
}