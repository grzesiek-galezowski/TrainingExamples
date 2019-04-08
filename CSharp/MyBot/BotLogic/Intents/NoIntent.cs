using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class NoIntent : IIntent
  {
    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnNoAsync(cancellationToken);
    }
  }
}