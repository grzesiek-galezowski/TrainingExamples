using System.Threading.Tasks;
using BotLogic;

namespace BotBuilderEchoBotV4.Logic
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