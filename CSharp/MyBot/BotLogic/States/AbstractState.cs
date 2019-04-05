using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;

namespace BotLogic.States
{
  public abstract class AbstractState : IState
  {
    public virtual Task OnEnterAsync(IConversationPartner conversationPartner)
    {
      return Task.CompletedTask;
    }

    public virtual async Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine,
      CancellationToken cancellationToken)
    {
      conversationPartner.AppendToResponse("Nothing to confirm.");
    }

    public virtual async Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine,
      CancellationToken cancellationToken)
    {
      conversationPartner.AppendToResponse("Nothing to reject.");
    }

    public virtual async Task OnStartGameAsync(IDialogContext dialogContext, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      conversationPartner.AppendToResponse("You are mid-game, right?");
    }

    public virtual Task OnKillCharacterAsync(IDialogContext dialogContext,
      ICharacter character,
      IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}