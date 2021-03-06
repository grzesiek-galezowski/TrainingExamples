using System;
using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace BotLogic.Characters
{
  public class Aragorn : ICharacter
  {
    public void TryToKill(IPlayer player)
    {
      player.AppendToResponse(Roles.Narrator, BotPhrases.AttemptingToKillAragornAnswer());
    }

    public Task TalkToAsync(
      IDialogContext dialogContext,
      IPlayer player,
      CancellationToken cancellationToken)
    {
      player.AppendToResponse(Roles.Aragorn, BotPhrases.QuestionFromAragornAboutFrodosStateOfMind());
      return dialogContext.GoToAsync(StateNames.AragornAsksAboutFrodosFianceeName, cancellationToken);
    }
  }
}