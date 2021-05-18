using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotLogic;
using Microsoft.Bot.Builder;

namespace GameBot.Adapters
{
  public class BotBuilderPlayer : IPlayer
  {
    private readonly ITurnContext _turnContext;
    private readonly List<(string, string)> _responseMessage = new List<(string, string)>();

    public BotBuilderPlayer(ITurnContext turnContext)
    {
      _turnContext = turnContext;
    }

    public void AppendToResponse(string role, string text)
    {
      if (_responseMessage.Any() && _responseMessage.Last().Item1 == role)
      {
        var valueTuple = _responseMessage[_responseMessage.Count - 1];
        _responseMessage[_responseMessage.Count - 1] = (valueTuple.Item1, valueTuple.Item2 + text);
      }
      else
      {
        _responseMessage.Add((role, text));
      }
    }

    public Task RespondAsync(CancellationToken cancellationToken)
    {
      var text = _responseMessage.Select(((string role, string text) pair) => pair.role + ": " + pair.text).ToArray();
      return _turnContext.SendActivityAsync(string.Join(Environment.NewLine + Environment.NewLine, text), cancellationToken: cancellationToken);
    }
  }

 
}