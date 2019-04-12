using System.Threading;
using System.Threading.Tasks;
using BotLogic.Intents;

namespace BotLogic
{
  public class InvalidItent : IIntent
  {
    private readonly IPlayer _player;

    public InvalidItent(IPlayer player)
    {
      _player = player;
    }

    public async Task ApplyToAsync(
      IDialogStateMachine dialogStateMachine,
      CancellationToken cancellationToken)
    {
      _player.AppendToResponse(Roles.Narrator, "Invalid intent, sorry!");
    }
  }
}