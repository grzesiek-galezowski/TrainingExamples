using System.Threading.Tasks;

namespace BotLogic.States
{
    public class DisplayingGameCatalogState : IState
    {
        private readonly GameCatalog _gameCatalog;

        public DisplayingGameCatalogState(GameCatalog gameCatalog)
        {
            _gameCatalog = gameCatalog;
        }

        public async Task OnWatchGameCatalogAsync(IUser user, IDialogContext dialogContext)
        {
            user.AppendToResponse("You are already watching game catalog.");
        }

        public async Task OnEnterAsync(IUser user)
        {
            var games = await _gameCatalog.GetGamesAsync();
            games.DisplayFor(user);
        }

        public Task OnGoShoppingAsync(IUser user, IDialogContext dialogStateMachine)
        {
            return dialogStateMachine.GoToAsync(States.FromGameCatalogToDisplayShop, user);
        }

        public async Task OnYesAsync(IUser user, IDialogContext dialogStateMachine)
        {
            user.AppendToResponse("There's nothing to confirm");
        }

        public async Task OnNoAsync(IUser user, IDialogContext dialogStateMachine)
        {
            user.AppendToResponse("There's nothing to reject");
        }
    }
}