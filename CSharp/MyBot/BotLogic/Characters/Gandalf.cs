using System;
using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace BotLogic.Characters
{
  public class Gandalf : ICharacter
  {
    public void TryToKill(IPlayer player)
    {
      player.AppendToResponse(Roles.Narrator, BotPhrases.AttemptingToKillGandalfAnswer());
    }

    public Task TalkToAsync(IDialogContext dialogContext,
      IPlayer player,
      CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}