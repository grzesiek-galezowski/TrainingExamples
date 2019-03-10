using System.Threading.Tasks;

namespace BotLogic.States
{

    public class DisplayingShopState : IState
    {
        public Task OnWatchGameCatalogAsync(IUser user, IDialogContext dialogContext)
        {
            throw new System.NotImplementedException();
        }

        public Task OnEnterAsync(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task OnGoShoppingAsync(IUser user, IDialogContext dialogStateMachine)
        {
            throw new System.NotImplementedException();
        }

        public Task OnYesAsync(IUser user, IDialogContext dialogStateMachine)
        {
            throw new System.NotImplementedException();
        }

        public Task OnNoAsync(IUser user, IDialogContext dialogStateMachine)
        {
            throw new System.NotImplementedException();
        }
    }
}