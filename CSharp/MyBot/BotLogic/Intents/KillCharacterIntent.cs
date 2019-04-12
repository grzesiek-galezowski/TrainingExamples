using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;

namespace BotLogic.Intents
{
  public class KillCharacterIntent : IIntent
  {
    private readonly ICharacter _character;

    public KillCharacterIntent(ICharacter character)
    {
      _character = character;
    }

    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnKillCharacterAsync(_character, cancellationToken);
    }
  }
}