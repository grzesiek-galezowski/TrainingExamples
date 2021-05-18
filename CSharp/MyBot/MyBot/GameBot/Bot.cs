// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using BotLogic;
using BotLogic.Composition;
using GameBot.Adapters;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace GameBot
{
  public class Bot : IBot
  {
    private readonly IActivityFactory _activityFactory;
    private readonly ITurnContextPoweredObjectsFactory _turnContextPoweredObjectsFactory;

    public Bot(
      IActivityFactory activityFactory,
      ITurnContextPoweredObjectsFactory turnContextPoweredObjectsFactory)
    {
      _activityFactory = activityFactory;
      _turnContextPoweredObjectsFactory = turnContextPoweredObjectsFactory;
    }

    public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
    {
      var userPhrase = _turnContextPoweredObjectsFactory.ExtractUserPhraseFrom(turnContext);
      var botPersistentState = _turnContextPoweredObjectsFactory.CreateBotPersistentState(turnContext);
      var partner = _turnContextPoweredObjectsFactory.CreateConversationPartner(turnContext);

      if (turnContext.Activity.Type == ActivityTypes.Message)
      {
        var messageActivity = await _activityFactory.CreateMessageActivityAsync(
          botPersistentState,
          userPhrase,
          partner,
          cancellationToken);
        await messageActivity.HandleAsync(cancellationToken);
      }
      else
      {
        partner.AppendToResponse(Roles.Narrator, $"{turnContext.Activity.Type} event detected");
      }

      await partner.RespondAsync(cancellationToken);
      await botPersistentState.CommitChangesAsync(cancellationToken);
    }
  }

}
