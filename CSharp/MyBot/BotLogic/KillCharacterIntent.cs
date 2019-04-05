using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;
using BotLogic.Intents;

namespace BotLogic
{
  public class KillCharacterIntent : IIntent
  {
    private readonly ICharacter _character;

    public KillCharacterIntent(ICharacter character)
    {
      _character = character;
    }

    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnKillCharacterAsync(_character, conversationPartner, cancellationToken);
    }
  }
}