using System.Threading.Tasks;

namespace BotLogic.States
{
    public interface IState
    {
        Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext);
        Task OnEnterAsync(IConversationPartner conversationPartner);
        Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine);
        Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine);
        Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine);
    }

    public abstract class DefaultState : IState
    {
      public virtual async Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext)
      {
        conversationPartner.AppendToResponse("You cannot watch game catalog now.");
      }

      public abstract Task OnEnterAsync(IConversationPartner conversationPartner);


      public virtual async Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
      {
        conversationPartner.AppendToResponse("You cannot go shopping now.");
      }

      public virtual async Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
      {
        conversationPartner.AppendToResponse("Nothing to confirm.");
      }

      public virtual async Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
      {
        conversationPartner.AppendToResponse("Nothing to reject.");
      }
    }
}