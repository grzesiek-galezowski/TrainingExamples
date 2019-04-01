using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace BotLogic
{

    public class DialogStateMachine : IDialogContext
    {
        private readonly IStatesFactory _states;
        private readonly IBotPersistentState _persistentState;
        private IState _currentState;

        public DialogStateMachine(
            IState currentState,
            IStatesFactory states,
            IBotPersistentState persistentState)
        {
            _currentState = currentState;
            _states = states;
            _persistentState = persistentState;
        }


        public async Task GoToAsync(States.States state, IConversationPartner conversationPartner, CancellationToken cancellationToken)
        {
            _currentState = _states.GetState(state);
            await _persistentState.SetCurrentStateAsync(state, cancellationToken);
            await _currentState.OnEnterAsync(conversationPartner);
        }

        public Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken)
        {
            return _currentState.OnWatchGameCatalogAsync(conversationPartner, this, cancellationToken);
        }

        public Task OnGoShoppingIntentAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken)
        {
            return _currentState.OnGoShoppingAsync(conversationPartner, this, cancellationToken);
        }

        public Task OnYesAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken)
        {
            return _currentState.OnYesAsync(conversationPartner, this, cancellationToken);
        }

        public Task OnNoAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken)
        {
            return _currentState.OnNoAsync(conversationPartner, this, cancellationToken);
        }
    }
}