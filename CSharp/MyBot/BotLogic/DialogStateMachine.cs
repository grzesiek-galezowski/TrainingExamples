using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace BotLogic
{
  public interface IDialogStateMachine
  {
    Task OnYesAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken);
    Task OnNoAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken);
    Task OnStartGameAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken);
    Task OnKillCharacterAsync(string characterName, IConversationPartner conversationPartner, CancellationToken cancellationToken);
  }

  public class DialogStateMachine : IDialogContext, IDialogStateMachine
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


    public async Task GoToAsync(StateNames stateName, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      _currentState = _states.GetState(stateName);
      await _persistentState.SetCurrentStateAsync(stateName, cancellationToken);
      await _currentState.OnEnterAsync(conversationPartner);
    }

    public Task OnYesAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken)
    {
      return _currentState.OnYesAsync(conversationPartner, this, cancellationToken);
    }

    public Task OnNoAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken)
    {
      return _currentState.OnNoAsync(conversationPartner, this, cancellationToken);
    }

    public Task OnStartGameAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken)
    {
      return _currentState.OnStartGameAsync((IDialogContext)this, conversationPartner, cancellationToken);
    }

    public Task OnKillCharacterAsync(string characterName, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return _currentState.OnKillCharacterAsync((IDialogContext)this, new Gandalf(), conversationPartner, cancellationToken);
    }
  }
}