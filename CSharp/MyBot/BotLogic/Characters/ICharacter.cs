using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace BotLogic.Characters
{
  public interface ICharacter
  {
    void TryToKill(IPlayer player);
    Task TalkToAsync(IDialogContext dialogContext,
      IPlayer player,
      CancellationToken cancellationToken);
  }
}