using System.Threading.Tasks;

namespace BotLogic
{
  public interface IUser
  {
    Task AppendToResponseAsync(string text);
    Task RespondAsync();
  }
}