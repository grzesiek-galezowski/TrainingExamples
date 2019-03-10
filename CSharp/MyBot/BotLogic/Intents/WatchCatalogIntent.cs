using System.Threading.Tasks;

namespace BotLogic
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