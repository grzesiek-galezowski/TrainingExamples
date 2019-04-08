using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public class YesNoTransitionState : AbstractState
  {
    public YesNoTransitionState(IPlayer player) : base(player)
    {
    }
  }
}