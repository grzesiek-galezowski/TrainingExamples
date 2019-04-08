using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  public interface IIntent
    {
        Task ApplyToAsync(IDialogStateMachine dialogStateMachine, CancellationToken cancellationToken);
    }
}