using System;
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

    public override Task OnSomeWordsAsync(IDialogContext context, Words words, in CancellationToken cancellationToken)
    {
      if(words.AsSpaceSeparatedString() == "Aragorn")
      {
        _player.AppendToResponse(BotPhrases.AragornJokesAboutHimBeingAFianceeOfFrodo());
        return context.GoToAsync(StateNames.AragornAsksAboutFrodosFianceeName, cancellationToken);
      }
      else
      {
        _player.AppendToResponse(BotPhrases.AragornsStoryOfHisFianceeAfterAcknowleding(words.AsSpaceSeparatedString()));
        return context.GoToAsync(StateNames.EnterBrightRoomState, cancellationToken);
      }
    }

    public override async Task OnQuestionWhoAsync(IDialogContext context, CancellationToken cancellationToken)
    {
      _player.AppendToResponse(BotPhrases.ClarificationFromAragornAboutFrodosFianceeName());

    }
  }
}