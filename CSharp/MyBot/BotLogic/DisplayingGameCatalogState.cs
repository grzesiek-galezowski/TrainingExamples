using System.Threading.Tasks;
using BotLogic;

namespace BotBuilderEchoBotV4.Logic
{
    public class DisplayingGameCatalogState : IState
    {
        private readonly GameCatalog _gameCatalog;

        public DisplayingGameCatalogState(GameCatalog gameCatalog)
        {
            _gameCatalog = gameCatalog;
        }

        public Task OnWatchGameCatalogAsync(IUser user, DialogStateMachine dialogContext)
        {
            user.AppendToResponseAsync("You are already watching game catalog.");
            return Task.CompletedTask;
        }

        public async Task OnEnterAsync(IUser user)
        {
            var games = await _gameCatalog.GetGamesAsync();
            games.DisplayFor(user);
        }

        public Task OnGoShoppingAsync(IUser user, DialogStateMachine dialogStateMachine)
        {
            return dialogStateMachine.GoToAsync(States.FromGameCatalogToDisplayShop, user);
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