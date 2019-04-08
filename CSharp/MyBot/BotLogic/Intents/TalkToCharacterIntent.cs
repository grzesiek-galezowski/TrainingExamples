using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;

namespace BotLogic.Intents
{
  public class TalkToCharacterIntent : IIntent
  {
    private readonly ICharacter _character;

    public TalkToCharacterIntent(ICharacter character)
    {
      _character = character;
    }

    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnTalkToAsync(_character, cancellationToken);
    }
  }
}