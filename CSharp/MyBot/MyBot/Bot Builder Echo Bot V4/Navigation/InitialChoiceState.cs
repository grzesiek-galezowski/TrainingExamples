using System.Threading.Tasks;

namespace BotBuilderEchoBotV4.Navigation
{
    public class InitialChoiceState : IState
    {
        public Task OnWatchGameCatalogAsync(User user, DialogStateMachine dialogContext)
        {
            return dialogContext.GoToAsync(States.DisplayingCatalog, user);
        }

        public Task OnEnterAsync(User user)
        {
            return Task.CompletedTask;
        }

        public Task OnGoShoppingAsync(User user, DialogStateMachine dialogStateMachine)
        {
            return dialogStateMachine.GoToAsync(States.DisplayingShop, user);
        }

        public Task OnYesAsync(User user, DialogStateMachine dialogStateMachine)
        {
            return user.SayAsync("There's nothing to confirm");
        }

        public Task OnNoAsync(User user, DialogStateMachine dialogStateMachine)
        {
            return user.SayAsync("There's nothing to reject");
        }

    }
    }
