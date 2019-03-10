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


        public async Task GoToAsync(States.States state, IUser user)
        {
            _currentState = _states.GetState(state);
            await _persistentState.SetCurrentStateAsync(state);
            await _currentState.OnEnterAsync(user);
        }

        public Task OnWatchGameCatalogAsync(IUser user)
        {
            return _currentState.OnWatchGameCatalogAsync(user, this);
        }

        public Task OnGoShoppingIntentAsync(IUser user)
        {
            return _currentState.OnGoShoppingAsync(user, this);
        }

        public Task OnYesAsync(IUser user)
        {
            return _currentState.OnYesAsync(user, this);
        }

        public Task OnNoAsync(IUser user)
        {
            return _currentState.OnNoAsync(user, this);
        }
    }
}