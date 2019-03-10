using System.Threading.Tasks;

namespace BotLogic
{
    public class DisplayingGameCatalogState : IState
    {
        private readonly GameCatalog _gameCatalog;

        public DisplayingGameCatalogState(GameCatalog gameCatalog)
        {
            _gameCatalog = gameCatalog;
        }

        public Task OnWatchGameCatalogAsync(IUser user, IDialogContext dialogContext)
        {
            user.AppendToResponseAsync("You are already watching game catalog.");
            return Task.CompletedTask;
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

        public Task OnYesAsync(IUser user, IDialogContext dialogStateMachine)
        {
            return user.AppendToResponseAsync("There's nothing to confirm");
        }

        public Task OnNoAsync(IUser user, IDialogContext dialogStateMachine)
        {
            return user.AppendToResponseAsync("There's nothing to reject");
        }
    }
}