using System;
using System.Threading.Tasks;
using BotBuilderEchoBotV4.Navigation;

namespace BotBuilderEchoBotV4
{
    internal class IntentRecognition
    {
        public IIntent From(string text)
        {
            if (text.Contains("catalog"))
            {
                return new WatchCatalogIntent(text);
            }
            else if (text.Contains("yes"))
            {
                return new YesIntent();
            }
            else if (text.Contains("no"))
            {
                return new NoIntent();
            }
            else
            {
                return new GoShoppingIntent();
            }
    
        }
    }

    internal class NoIntent : IIntent
    {
        public Task ApplyTo(DialogStateMachine dialogStateMachine, User user)
        {
            return dialogStateMachine.OnYesAsync(user);
        }
    }

    internal class YesIntent : IIntent
    {
        public Task ApplyTo(DialogStateMachine dialogStateMachine, User user)
        {
            return dialogStateMachine.OnNoAsync(user);
        }
    }

    internal class GoShoppingIntent : IIntent
    {
        public Task ApplyTo(DialogStateMachine dialogStateMachine, User user)
        {
            return dialogStateMachine.OnGoShoppingIntentAsync(user);
        }
    }
}