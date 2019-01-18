using System.Threading.Tasks;

namespace BotBuilderEchoBotV4.Navigation
{
    public class DisplayingGameCatalogState : IState
    {
        private readonly GameCatalog _gameCatalog;

        public DisplayingGameCatalogState(GameCatalog gameCatalog)
        {
            _gameCatalog = gameCatalog;
        }

        public Task OnWatchGameCatalogAsync(User user, DialogStateMachine dialogContext)
        {
            user.SayAsync("You are already watching game catalog.");
            return Task.CompletedTask;
        }

        public async Task OnEnterAsync(User user)
        {
            var games = await _gameCatalog.GetGamesAsync();
            games.DisplayFor(user);
        }

        public Task OnGoShoppingAsync(User user, DialogStateMachine dialogStateMachine)
        {
            return dialogStateMachine.GoToAsync(States.FromGameCatalogToDisplayShop, user);
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