using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;

namespace BotLogic.States
{
  public abstract class AbstractState : IState
  {
    private readonly IPlayer _player;

    protected AbstractState(IPlayer player)
    {
      _player = player;
    }

    public virtual Task OnEnterAsync(CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    public virtual async Task OnYesAsync(IDialogContext dialogStateMachine,
      CancellationToken cancellationToken)
    {
      _player.AppendToResponse("Nothing to confirm.");
    }

    public virtual async Task OnNoAsync(IDialogContext dialogStateMachine,
      CancellationToken cancellationToken)
    {
      _player.AppendToResponse("Nothing to reject.");
    }

    public virtual async Task OnStartGameAsync(IDialogContext dialogContext,
      CancellationToken cancellationToken)
    {
      _player.AppendToResponse("You are mid-game, right?");
    }

    public virtual async Task OnKillCharacterAsync(IDialogContext dialogContext,
      ICharacter character,
      CancellationToken cancellationToken)
    {
      _player.AppendToResponse("A strange voice says: Hold your horses, Mary Cooper!");
    }

    public virtual Task OnTalkToAsync(
      IDialogContext dialogContext, 
      ICharacter character,
      CancellationToken cancellationToken)
    {
      _player.AppendToResponse("No point in talking now");
      return Task.CompletedTask;
    }

    public virtual Task OnSomeWordsAsync(IDialogContext context,
      Words words,
      in CancellationToken cancellationToken)
    {
      _player.AppendToResponse("What does " + words.AsSpaceSeparatedString() + " even mean?");
      return Task.CompletedTask;
    }
  }
}


