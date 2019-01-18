using System.Threading.Tasks;
using BotBuilderEchoBotV4.Navigation;

namespace BotBuilderEchoBotV4
{
    internal interface IIntent
    {
        Task ApplyTo(DialogStateMachine dialogStateMachine, User user);
    }
}