using System.Threading.Tasks;

namespace BotLogic.States
{
  public class YesNoTransitionState : IState //bug rename
  {
    public Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext)
    {
      conversationPartner.AppendToResponse("OK, let's stay with the catalog");
      return dialogContext.GoToAsync(States.DisplayingCatalog, conversationPartner);
    }

    public async Task OnEnterAsync(IConversationPartner conversationPartner)
    {
      conversationPartner.AppendToResponse("Do you really want to stop viewing the catalog and go shopping?");
    }

    public Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
    {
      conversationPartner.AppendToResponse("OK, let's go shopping!");
      return dialogStateMachine.GoToAsync(States.DisplayingShop, conversationPartner);
    }

    public Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
    {
      return OnGoShoppingAsync(conversationPartner, dialogStateMachine);
    }

    public Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
    {
      return OnWatchGameCatalogAsync(conversationPartner, dialogStateMachine);
    }
  }
}