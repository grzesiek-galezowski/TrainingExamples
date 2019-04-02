using System;
using System.Threading;
using System.Threading.Tasks;
using BotLogic;
using Microsoft.Bot.Builder;

namespace GameBot.Adapters
{
  public class BotBuilderConversationPartner : IConversationPartner
  {
        private readonly ITurnContext _turnContext;
        private string _responseMessage = string.Empty;

        public BotBuilderConversationPartner(ITurnContext turnContext)
        {
            _turnContext = turnContext;
        }

        public void AppendToResponse(string text)
        {
            if (_responseMessage != string.Empty)
            {
                _responseMessage += Environment.NewLine;
            }

            _responseMessage += text;
        }

        public Task RespondAsync(CancellationToken cancellationToken)
        {
            return _turnContext.SendActivityAsync(_responseMessage, cancellationToken: cancellationToken);
        }
    }
}