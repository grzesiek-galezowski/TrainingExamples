// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using BotBuilderEchoBotV4.Adapters;
using BotBuilderEchoBotV4.Logic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace BotBuilderEchoBotV4
{
  public class GameStoreBot : IBot
  {
    private readonly EchoBotAccessors _accessors;
    private readonly ActivityFactory _activityFactory;

    public GameStoreBot(EchoBotAccessors accessors, ILoggerFactory loggerFactory, ActivityFactory activityFactory)
    {
      ILogger logger = loggerFactory.CreateLogger<GameStoreBot>();
      logger.LogTrace("EchoBot turn start.");
      _accessors = accessors;
      _activityFactory = activityFactory;
    }

    public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
    {
      var botPersistentState = new BotPersistentState(turnContext, _accessors);

      if (turnContext.Activity.Type == ActivityTypes.Message)
      {
        var messageActivity = await _activityFactory.CreateMessageActivity(
          botPersistentState, new BotBuilderConversationPartner(turnContext), turnContext.Activity.Text);
        await messageActivity.HandleAsync();
      }
      else
      {
        await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
      }

      await botPersistentState.CommittChangesAsync();
    }
  }
}
