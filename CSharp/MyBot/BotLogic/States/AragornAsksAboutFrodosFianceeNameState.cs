using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public class AragornAsksAboutFrodosFianceeNameState : AbstractState
  {
    private readonly IPlayer _player;

    public AragornAsksAboutFrodosFianceeNameState(IPlayer player) : base(player)
    {
      _player = player;
    }

    public override async Task OnEnterAsync(CancellationToken cancellationToken)
    {
      _player.AppendToResponse(BotPhrases.QuestionFromAragornAboutFrodosFianceeName());
    }

    public override Task OnSomeWordsAsync(IDialogContext context,
      IEnumerable<string> words, in CancellationToken cancellationToken)
    { 
      var frodosFianceeName = string.Join(" ", words);
      if(frodosFianceeName == "Aragorn")
      {
        _player.AppendToResponse(BotPhrases.AragornJokesAboutHimBeingAFianceeOfFrodo());
        return context.GoToAsync(StateNames.AragornAsksAboutFrodosFianceeName, cancellationToken);
      }
      else
      {
        _player.AppendToResponse(BotPhrases.AragornsStoryOfHisFianceeAfterAcknowleding(frodosFianceeName));
        return context.GoToAsync(StateNames.EnterBrightRoomState, cancellationToken);
      }
    }
  }
}