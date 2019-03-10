using System.Threading.Tasks;

namespace BotLogic
{
  public interface IDialogContext
    {
      Task GoToAsync(States.States state, IUser user);
    }
}