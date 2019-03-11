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


        public async Task GoToAsync(States.States state, IConversationPartner conversationPartner)
        {
            _currentState = _states.GetState(state);
            await _persistentState.SetCurrentStateAsync(state);
            await _currentState.OnEnterAsync(conversationPartner);
        }

        public Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner)
        {
            return _currentState.OnWatchGameCatalogAsync(conversationPartner, this);
        }

        public Task OnGoShoppingIntentAsync(IConversationPartner conversationPartner)
        {
            return _currentState.OnGoShoppingAsync(conversationPartner, this);
        }

        public Task OnYesAsync(IConversationPartner conversationPartner)
        {
            return _currentState.OnYesAsync(conversationPartner, this);
        }

        public Task OnNoAsync(IConversationPartner conversationPartner)
        {
            return _currentState.OnNoAsync(conversationPartner, this);
        }
    }
}