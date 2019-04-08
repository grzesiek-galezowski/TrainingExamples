using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class YesIntent : IIntent
  {
    public Task ApplyToAsync(
        IDialogStateMachine dialogStateMachine,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnYesAsync(cancellationToken);
    }
  }
}