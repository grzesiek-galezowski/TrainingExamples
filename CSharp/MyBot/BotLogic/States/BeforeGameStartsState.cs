using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public class BeforeGameStartsState : AbstractState
  {
    private readonly IPlayer _player;

    public BeforeGameStartsState(IPlayer player) : base(player)
    {
      _player = player;
    }

    public override async Task OnStartGameAsync(IDialogContext dialogContext,
      CancellationToken cancellationToken)
    {
      await dialogContext.GoToAsync(StateNames.EnterBrightRoomState, cancellationToken);
    }
  }
}
