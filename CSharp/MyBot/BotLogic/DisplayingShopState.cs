using System.Threading.Tasks;

namespace BotLogic
{

    public class DisplayingShopState : IState
    {
        public Task OnWatchGameCatalogAsync(IUser user, DialogStateMachine dialogContext)
        {
            throw new System.NotImplementedException();
        }

        public Task OnEnterAsync(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task OnGoShoppingAsync(IUser user, DialogStateMachine dialogStateMachine)
        {
            throw new System.NotImplementedException();
        }

        public Task OnYesAsync(IUser user, DialogStateMachine dialogStateMachine)
        {
            throw new System.NotImplementedException();
        }

        public Task OnNoAsync(IUser user, DialogStateMachine dialogStateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}