using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;
using BotLogic.States;

namespace BotLogic
{
  public interface IDialogStateMachine
  {
    Task OnStartGameAsync(CancellationToken cancellationToken);
    Task OnKillCharacterAsync(ICharacter character, CancellationToken cancellationToken);

    Task OnTalkToAsync(ICharacter character, CancellationToken cancellationToken);
    Task OnSomeWordsAsync(Words words, CancellationToken cancellationToken);
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


    public async Task GoToAsync(
      StateNames stateName,
      CancellationToken cancellationToken)
    {
      _currentState = _states.GetState(stateName);
      await _persistentState.SetCurrentStateAsync(stateName, cancellationToken);
      await _currentState.OnEnterAsync(cancellationToken);
    }

    public Task OnStartGameAsync(CancellationToken cancellationToken)
    {
      return _currentState.OnStartGameAsync(this, cancellationToken);
    }

    public Task OnKillCharacterAsync(
      ICharacter character,
      CancellationToken cancellationToken)
    {
      return _currentState.OnKillCharacterAsync(this, character, cancellationToken);
    }

    public Task OnTalkToAsync(ICharacter character, CancellationToken cancellationToken)
    {
      return _currentState.OnTalkToAsync(this, character, cancellationToken);
    }

    public Task OnSomeWordsAsync(Words words, CancellationToken cancellationToken)
    {
      return _currentState.OnSomeWordsAsync(this, words, cancellationToken);
    }
  }
}