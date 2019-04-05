using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;

namespace BotLogic.States
{
  public interface IState
  {
    Task OnEnterAsync(IConversationPartner conversationPartner);

    Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine,
      CancellationToken cancellationToken);

    Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine,
      CancellationToken cancellationToken);

    Task OnStartGameAsync(IDialogContext dialogContext, IConversationPartner conversationPartner,
      CancellationToken cancellationToken);
    Task OnKillCharacterAsync(IDialogContext dialogContext,
      ICharacter character,
      IConversationPartner conversationPartner, CancellationToken cancellationToken);
  }
}