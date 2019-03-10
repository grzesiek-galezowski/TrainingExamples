using System.Threading.Tasks;

namespace BotLogic
{
    public class InitialChoiceState : IState
    {
        public Task OnWatchGameCatalogAsync(IUser user, DialogStateMachine dialogContext)
        {
            return dialogContext.GoToAsync(States.DisplayingCatalog, user);
        }

        public Task OnEnterAsync(IUser user)
        {
            return Task.CompletedTask;
        }

        public Task OnGoShoppingAsync(IUser user, DialogStateMachine dialogStateMachine)
        {
            return dialogStateMachine.GoToAsync(States.DisplayingShop, user);
        }

        public Task OnYesAsync(IUser user, DialogStateMachine dialogStateMachine)
        {
            return user.AppendToResponseAsync("There's nothing to confirm");
        }

        public Task OnNoAsync(IUser user, DialogStateMachine dialogStateMachine)
        {
            return user.AppendToResponseAsync("There's nothing to reject");
        }

    }
    }
