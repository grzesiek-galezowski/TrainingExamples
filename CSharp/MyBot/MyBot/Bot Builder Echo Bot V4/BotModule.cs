using System;
using System.Linq;
using BotLogic.States;
using Functional.Maybe;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BotBuilderEchoBotV4
{
  public class BotModule
  {
    public Maybe<ConversationState> State { get; private set; }

    public void Configure(BotFrameworkOptions options, ILoggerFactory loggerFactory, bool isProduction, IConfiguration configuration)
    {
      ILogger logger = loggerFactory.CreateLogger<GameStoreBot>();

      var botConfig = ReadBotConfiguration(configuration);
      SetupCredentialsProvider(isProduction, options, botConfig);
      SetupFallbackErrorHandling(options, logger);

      State = new ConversationState(new MemoryStorage()).ToMaybe();
    }

    public BotAccessors CreateBotAccessors()
    {
      var conversationState = State.Value;
      var accessors = new BotAccessors(conversationState)
      {
        CurrentState = conversationState.CreateProperty<States>(BotAccessors.StatesName),
      };

      return accessors;
    }

    private void SetupFallbackErrorHandling(BotFrameworkOptions options, ILogger logger)
    {
      options.OnTurnError = async (context, exception) =>
      {
        logger.LogError($"Exception caught : {exception}");
        await context.SendActivityAsync(exception.ToString());
      };
    }

    private static EndpointService GetEndpointService(bool isProduction, BotConfiguration botConfig)
    {
      var environment = isProduction ? "production" : "development";
      var service = botConfig.Services.FirstOrDefault(s => s.Type == "endpoint" && s.Name == environment);
      if (!(service is EndpointService endpointService))
      {
        throw new InvalidOperationException($"The .bot file does not contain an endpoint with name '{environment}'.");
      }

      return endpointService;
    }

    private BotConfiguration ReadBotConfiguration(IConfiguration configuration)
    {
      var secretKey = configuration.GetSection("botFileSecret").Value;
      var botFilePath = configuration.GetSection("botFilePath").Value;

      var botConfig = BotConfiguration.Load(botFilePath ?? @".\BotConfiguration.bot", secretKey)
                      ?? throw new InvalidOperationException($"The .bot config file could not be loaded.");
      return botConfig;
    }

    private void SetupCredentialsProvider(bool isProduction, BotFrameworkOptions options, BotConfiguration botConfig)
    {
      var endpointService = GetEndpointService(isProduction, botConfig);
      options.CredentialProvider = new SimpleCredentialProvider(endpointService.AppId, endpointService.AppPassword);
    }

    private static void AssertOptionsExist(IServiceProvider sp)
    {
      var _ = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value ?? throw new InvalidOperationException(
                "BotFrameworkOptions must be configured prior to setting up the state accessors");
    }
  }
}