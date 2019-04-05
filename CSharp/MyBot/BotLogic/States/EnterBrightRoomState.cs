using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;

namespace BotLogic.States
{
  public class EnterBrightRoomState : AbstractState
  {
    public override async Task OnEnterAsync(IConversationPartner conversationPartner)
    {
      conversationPartner.AppendToResponse(BotPhrases.EntryDescription());
    }

    public override async Task OnKillCharacterAsync(
      IDialogContext dialogContext,
      ICharacter character,
      IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      character.TryToKill(conversationPartner);
      await dialogContext.GoToAsync(StateNames.BeforeGameStarts, conversationPartner, cancellationToken);
    }
  }
}