using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;

namespace BotLogic.States
{
  public class EnterBrightRoomState : AbstractState
  {
    private readonly IPlayer _player;

    public EnterBrightRoomState(IPlayer player) : base(player)
    {
      _player = player;
    }

    public override async Task OnEnterAsync(CancellationToken cancellationToken)
    {
      _player.AppendToResponse(BotPhrases.EntryDescription());
    }

    public override async Task OnKillCharacterAsync(
      IDialogContext dialogContext,
      ICharacter character,
      CancellationToken cancellationToken)
    {
      character.TryToKill(_player);
      await dialogContext.GoToAsync(StateNames.BeforeGameStarts, cancellationToken);
    }

    public override Task OnTalkToAsync(IDialogContext dialogContext, ICharacter character,
      CancellationToken cancellationToken)
    {
      return character.TalkToAsync(dialogContext, _player, cancellationToken);
    }
  }
}