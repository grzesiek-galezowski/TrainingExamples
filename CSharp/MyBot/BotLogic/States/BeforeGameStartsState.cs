using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public class BeforeGameStartsState : AbstractState
  {
    public override async Task OnStartGameAsync(IConversationPartner conversationPartner, CancellationToken cancellationToken)
    {
      conversationPartner.AppendToResponse(BrightRoomConversations.EntryDescription());
      //bug move to another state
    }
  }
}
