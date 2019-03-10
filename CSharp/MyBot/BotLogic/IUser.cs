using System.Threading.Tasks;

namespace BotLogic
{
  public interface IUser
  {
    void AppendToResponse(string text);
    Task RespondAsync();
  }
}