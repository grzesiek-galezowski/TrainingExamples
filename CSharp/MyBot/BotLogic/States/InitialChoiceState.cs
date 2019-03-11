using System.Threading.Tasks;

namespace BotLogic.States
{
  public class InitialChoiceState : IState
  {
    public Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext)
    {
      return dialogContext.GoToAsync(States.DisplayingCatalog, conversationPartner);
    }

    public Task OnEnterAsync(IConversationPartner conversationPartner)
    {
      return Task.CompletedTask;
    }

    public Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
    {
      return dialogStateMachine.GoToAsync(States.DisplayingShop, conversationPartner);
    }

    public async Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
    {
      conversationPartner.AppendToResponse("There's nothing to confirm");
    }

    public async Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine)
    {
      conversationPartner.AppendToResponse("There's nothing to reject");
    }

  }
}
