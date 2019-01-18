using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace BotBuilderEchoBotV4
{
    public class User
    {
        private readonly ITurnContext _turnContext;
        private string _responseMessage = string.Empty;

        public User(ITurnContext turnContext)
        {
            _turnContext = turnContext;
        }

        public Task SayAsync(string text)
        {
            return Task.Factory.StartNew(() =>
            {
                if (_responseMessage != string.Empty)
                {
                    _responseMessage += Environment.NewLine;
                }

                _responseMessage += text;
            });
        }

        public Task RespondAsync()
        {
            return _turnContext.SendActivityAsync(_responseMessage);
        }
    }
}