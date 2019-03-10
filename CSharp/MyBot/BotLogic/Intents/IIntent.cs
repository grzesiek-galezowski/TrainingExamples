using System.Threading.Tasks;

namespace BotLogic.Intents
{
  public interface IIntent
    {
        Task ApplyToAsync(DialogStateMachine dialogStateMachine, IUser user);
    }
}