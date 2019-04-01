using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.StateValues
{
    public interface IState
    {
        Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext, CancellationToken cancellationToken);
        Task OnEnterAsync(IConversationPartner conversationPartner);
        Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken token);
        Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken cancellationToken);
        Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken cancellationToken);
    }

    public abstract class DefaultState : IState
    {
      public virtual async Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext, CancellationToken cancellationToken)
      {
        conversationPartner.AppendToResponse("You cannot watch game catalog now.");
      }

      public abstract Task OnEnterAsync(IConversationPartner conversationPartner);


      public virtual async Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken token)
      {
        conversationPartner.AppendToResponse("You cannot go shopping now.");
      }

      public virtual async Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken cancellationToken)
      {
        conversationPartner.AppendToResponse("Nothing to confirm.");
      }

      public virtual async Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken cancellationToken)
      {
        conversationPartner.AppendToResponse("Nothing to reject.");
      }
    }
}