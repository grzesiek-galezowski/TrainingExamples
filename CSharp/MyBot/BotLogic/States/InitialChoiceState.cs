using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.StateValues
{
  public class InitialChoiceState : IState
  {
    public Task OnWatchGameCatalogAsync(IConversationPartner conversationPartner, IDialogContext dialogContext, CancellationToken cancellationToken)
    {
      return dialogContext.GoToAsync(States.DisplayingCatalog, conversationPartner, cancellationToken);
    }

    public Task OnEnterAsync(IConversationPartner conversationPartner)
    {
      return Task.CompletedTask;
    }

    public Task OnGoShoppingAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken token)
    {
      return dialogStateMachine.GoToAsync(States.DisplayingShop, conversationPartner, token);
    }

    public async Task OnYesAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken cancellationToken)
    {
      conversationPartner.AppendToResponse("There's nothing to confirm");
    }

    public async Task OnNoAsync(IConversationPartner conversationPartner, IDialogContext dialogStateMachine, CancellationToken cancellationToken)
    {
      conversationPartner.AppendToResponse("There's nothing to reject");
    }

  }
}
