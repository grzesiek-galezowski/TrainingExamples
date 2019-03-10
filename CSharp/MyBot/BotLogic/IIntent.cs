using System.Threading.Tasks;
using BotLogic;

namespace BotBuilderEchoBotV4.Logic
{
  public interface IIntent
    {
        Task ApplyTo(DialogStateMachine dialogStateMachine, IUser user);
    }
}