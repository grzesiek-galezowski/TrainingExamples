using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace BotLogic
{
  public interface IDialogContext
    {
      Task GoToAsync(StateNames stateName, CancellationToken cancellationToken);
    }
}