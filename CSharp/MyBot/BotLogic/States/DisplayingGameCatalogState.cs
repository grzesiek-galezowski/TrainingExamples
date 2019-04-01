using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
    public class DisplayingGameCatalogState : DefaultState
    {
        private readonly GameCatalog _gameCatalog;

        public DisplayingGameCatalogState(GameCatalog gameCatalog)
        {
            _gameCatalog = gameCatalog;
        }

        public override async Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext, CancellationToken cancellationToken)
        {
            conversationPartner.AppendToResponse("You are already watching game catalog.");
        }

        public override async Task OnEnterAsync(IConversationPartner conversationPartner)
        {
            var games = await _gameCatalog.GetGamesAsync();
            games.DisplayFor(conversationPartner);
        }

        public override Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken token)
        {
            return dialogStateMachine.GoToAsync(States.FromGameCatalogToDisplayShop, conversationPartner, token);
        }
    }
}