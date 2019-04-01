// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bot.Builder.Community.Recognizers.Fuzzy;
using BotLogic;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Schema;

namespace BotBuilderEchoBotV4
{
  public class GameStoreBot : IBot
  {
    private readonly IActivityFactory _activityFactory;
    private readonly ITurnContextPoweredObjectsFactory _turnContextPoweredObjectsFactory;

    public GameStoreBot(
      IActivityFactory activityFactory,
      ITurnContextPoweredObjectsFactory turnContextPoweredObjectsFactory)
    {
      _activityFactory = activityFactory;
      _turnContextPoweredObjectsFactory = turnContextPoweredObjectsFactory;
    }

    public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
    {
      var intentRecognizer = _turnContextPoweredObjectsFactory.ExtractUserPhraseFrom(turnContext);
      var botPersistentState = _turnContextPoweredObjectsFactory.CreateBotPersistentState(turnContext);
      var partner = _turnContextPoweredObjectsFactory.CreateConversationPartner(turnContext);

      if (turnContext.Activity.Type == ActivityTypes.Message)
      {
        var messageActivity = await _activityFactory.CreateMessageActivityAsync(
          botPersistentState,
          intentRecognizer,
          partner,
          cancellationToken);
        await messageActivity.HandleAsync(cancellationToken);
      }
      else
      {
        partner.AppendToResponse($"{turnContext.Activity.Type} event detected");
      }

      await partner.RespondAsync(cancellationToken);
      await botPersistentState.CommitChangesAsync(cancellationToken);
    }
  }

}
