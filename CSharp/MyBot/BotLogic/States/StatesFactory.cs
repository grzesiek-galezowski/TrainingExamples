using System;
using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public class StatesFactory : IStatesFactory
  {

    public StatesFactory()
    {
    }

    public IState GetState(States.StateNames stateName)
    {
      if (stateName == States.StateNames.BeforeGameStarts)
      {
        return new BeforeGameStartsState();
      }
      else if (stateName == States.StateNames.EnterBrightRoomState)
      {
        return new EnterBrightRoomState();
      }
      else
      {
          throw new Exception("trolololo");
      }
    }
  }

  public class EnterBrightRoomState : AbstractState
  {
    public override async Task OnEnterAsync(IConversationPartner conversationPartner)
    {
      conversationPartner.AppendToResponse(BrightRoomConversations.EntryDescription());
    }

    public override async Task OnKillCharacterAsync(
      IDialogContext dialogContext,
      ICharacter gandalf,
      IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      gandalf.TryToKill(conversationPartner);
      await dialogContext.GoToAsync(StateNames.BeforeGameStarts, conversationPartner, cancellationToken);
    }
  }
}