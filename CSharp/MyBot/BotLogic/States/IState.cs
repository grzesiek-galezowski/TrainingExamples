using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;

namespace BotLogic.States
{
  public interface IState
  {
    Task OnEnterAsync(CancellationToken cancellationToken);

    Task OnYesAsync(IDialogContext dialogStateMachine,
      CancellationToken cancellationToken);

    Task OnNoAsync(IDialogContext dialogStateMachine,
      CancellationToken cancellationToken);

    Task OnStartGameAsync(IDialogContext dialogContext,
      CancellationToken cancellationToken);
    Task OnKillCharacterAsync(
      IDialogContext dialogContext,
      ICharacter character, 
      CancellationToken cancellationToken);

    Task OnTalkToAsync(IDialogContext dialogContext, ICharacter character, CancellationToken cancellationToken);
    Task OnSomeWordsAsync(IDialogContext context, Words words, in CancellationToken cancellationToken);
  }
}