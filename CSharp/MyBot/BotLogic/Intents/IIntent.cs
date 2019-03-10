using System.Threading.Tasks;

namespace BotLogic
{
  public interface IIntent
    {
        Task ApplyTo(DialogStateMachine dialogStateMachine, IUser user);
    }
}