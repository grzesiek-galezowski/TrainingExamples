using System;
using System.Threading.Tasks;
using BotLogic;
using Microsoft.Bot.Builder;

namespace BotBuilderEchoBotV4.Logic
{
  public class BotBuilderUser : IUser
  {
        private readonly ITurnContext _turnContext;
        private string _responseMessage = string.Empty;

        public BotBuilderUser(ITurnContext turnContext)
        {
            _turnContext = turnContext;
        }

        public Task AppendToResponseAsync(string text)
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