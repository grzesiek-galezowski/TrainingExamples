// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using BotBuilderEchoBotV4.Navigation;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace BotBuilderEchoBotV4
{
  public class ConversationTurnContext
  {
    public ConversationTurnContext(ITurnContext returnValue)
    {
      ReturnValue = returnValue;
    }

    public ITurnContext ReturnValue { get; private set; }
  }

  /// <summary>
  /// Represents a bot that processes incoming activities.
  /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
  /// This is a Transient lifetime service.  Transient lifetime services are created
  /// each time they're requested. For each Activity received, a new instance of this
  /// class is created. Objects that are expensive to construct, or have a lifetime
  /// beyond the single turn, should be carefully managed.
  /// For example, the <see cref="MemoryStorage"/> object and associated
  /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
  /// </summary>
  /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
  public class EchoWithCounterBot : IBot
  {
    private readonly EchoBotAccessors _accessors;
    private readonly ILogger _logger;

    public EchoWithCounterBot(EchoBotAccessors accessors, ILoggerFactory loggerFactory)
    {
      _logger = loggerFactory.CreateLogger<EchoWithCounterBot>();
      _logger.LogTrace("EchoBot turn start.");
      _accessors = accessors;
    }

    public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
    {
      var botPersistentState = new BotPersistentState(turnContext, _accessors);
      // Handle Message activity type, which is the main activity type for shown within a conversational interface
      // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
      // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
      if (turnContext.Activity.Type == ActivityTypes.Message)
      {
        IStatesFactory states = new StatesFactory();
        await new MessageActivity(
          new User(turnContext),
          turnContext.Activity.Text,
          new IntentRecognition(),
          new DialogStateMachine(
            states.GetState(await botPersistentState.ReadCurrentStateAsync()),
            states,
            botPersistentState
            )).HandleAsync();
      }
      else
      {
        await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
      }

      botPersistentState.CommitChangesAsync();
    }
  }
}
