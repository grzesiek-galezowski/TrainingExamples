using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
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