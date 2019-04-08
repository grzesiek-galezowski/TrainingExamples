using System.Threading;
using System.Threading.Tasks;
using BotLogic.Intents;

namespace BotLogic
{
  public class StartGameIntent : IIntent
  {
    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnStartGameAsync(cancellationToken);
    }
  }
}