using System.Threading.Tasks;

namespace BotLogic
{
  public interface IDialogContext
    {
      Task GoToAsync(States state, IUser user);
    }
}