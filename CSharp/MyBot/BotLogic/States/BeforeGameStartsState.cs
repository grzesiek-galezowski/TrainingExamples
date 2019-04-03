using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public class BeforeGameStartsState : AbstractState
  {
    public override async Task OnStartGameAsync(IDialogContext dialogContext, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      await dialogContext.GoToAsync(StateNames.EnterBrightRoomState, conversationPartner, cancellationToken);
    }
  }
}
