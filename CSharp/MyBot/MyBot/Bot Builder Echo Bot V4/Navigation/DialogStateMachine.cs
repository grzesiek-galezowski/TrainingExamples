using System.Threading.Tasks;

namespace BotBuilderEchoBotV4.Navigation
{

    public class DialogStateMachine : IDialogContext
    {
        private readonly IStatesFactory _states;
        private readonly BotPersistentState _persistentState;
        private IState _currentState;

        public DialogStateMachine(
            IState currentState,
            IStatesFactory states,
            BotPersistentState persistentState)
        {
            _currentState = currentState;
            _states = states;
            _persistentState = persistentState;
        }


        public async Task GoToAsync(States state, User user)
        {
            _currentState = _states.GetState(state);
            await _persistentState.SetCurrentStateAsync(state);
            await _currentState.OnEnterAsync(user);
        }

        public Task OnWatchGameCatalogAsync(User user)
        {
            return _currentState.OnWatchGameCatalogAsync(user, this);
        }

        public Task OnGoShoppingIntentAsync(User user)
        {
            return _currentState.OnGoShoppingAsync(user, this);
        }

        public Task OnYesAsync(User user)
        {
            return _currentState.OnYesAsync(user, this);
        }

        public Task OnNoAsync(User user)
        {
            return _currentState.OnNoAsync(user, this);
        }
    }
}