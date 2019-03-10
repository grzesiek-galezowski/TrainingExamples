using System.Threading.Tasks;
using BotLogic;

namespace BotBuilderEchoBotV4.Logic
{
    internal class WatchCatalogIntent : IIntent
    {
        private readonly string _text;

        public WatchCatalogIntent(string text)
        {
            _text = text;
        }

        public Task ApplyTo(DialogStateMachine dialogStateMachine, IUser user)
        {
            return dialogStateMachine.OnWatchGameCatalogAsync(user);
        }
    }
}