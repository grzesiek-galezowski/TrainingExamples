using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public interface IState
  {
    Task OnEnterAsync(IConversationPartner conversationPartner);

    Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine,
      CancellationToken cancellationToken);

    Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine,
      CancellationToken cancellationToken);

    Task OnStartGameAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken);
    Task OnKillCharacterAsync(string characterName, IConversationPartner conversationPartner, CancellationToken cancellationToken);
  }
}