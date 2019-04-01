using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.StateValues
{
  public class YesNoTransitionState : IState //bug rename
  {
    public Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext, CancellationToken cancellationToken)
    {
      conversationPartner.AppendToResponse("OK, let's stay with the catalog");
      return dialogContext.GoToAsync(States.States.DisplayingCatalog, conversationPartner, cancellationToken);
    }

    public async Task OnEnterAsync(IConversationPartner conversationPartner)
    {
      conversationPartner.AppendToResponse("Do you really want to stop viewing the catalog and go shopping?");
    }

    public Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken token)
    {
      conversationPartner.AppendToResponse("OK, let's go shopping!");
      return dialogStateMachine.GoToAsync(States.States.DisplayingShop, conversationPartner, token);
    }

    public Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken cancellationToken)
    {
      return OnGoShoppingAsync(conversationPartner, dialogStateMachine, cancellationToken);
    }

    public Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken cancellationToken)
    {
      return OnWatchGameCatalogAsync(conversationPartner, dialogStateMachine, cancellationToken);
    }
  }
}