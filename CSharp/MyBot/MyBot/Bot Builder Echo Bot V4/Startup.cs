// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using BotBuilderEchoBotV4.Logic;
using BotLogic;
using BotLogic.States;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BotBuilderEchoBotV4
{
  public class Startup
  {
    private readonly bool _isProduction = false;
    private ILoggerFactory _loggerFactory;

    public Startup(IHostingEnvironment env)
    {
      _isProduction = env.IsProduction();
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddBot<GameStoreBot>(options => { ConfigureBot(services, options); });
      services.AddSingleton(CreateEchoBotAccessors);
      services.AddSingleton(new ActivityFactory());
    }

    private static EchoBotAccessors CreateEchoBotAccessors(IServiceProvider sp)
    {
      var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
      if (options == null)
      {
        throw new InvalidOperationException(
          "BotFrameworkOptions must be configured prior to setting up the state accessors");
      }

      var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
      if (conversationState == null)
      {
        throw new InvalidOperationException(
          "ConversationState must be defined and added before adding conversation-scoped state accessors.");
      }

      var accessors = new EchoBotAccessors(conversationState)
      {
        CurrentState = conversationState.CreateProperty<States>(EchoBotAccessors.StatesName),
      };

      return accessors;
    }

    private void ConfigureBot(IServiceCollection services, BotFrameworkOptions options)
    {
      ILogger logger = _loggerFactory.CreateLogger<GameStoreBot>();

      var secretKey = Configuration.GetSection("botFileSecret")?.Value;
      var botFilePath = Configuration.GetSection("botFilePath")?.Value;

      var botConfig = BotConfiguration.Load(botFilePath ?? @".\BotConfiguration.bot", secretKey) 
                      ?? throw new InvalidOperationException($"The .bot config file could not be loaded.");

      // Retrieve current endpoint.
      var endpointService = GetEndpointService(botConfig);
      SetupCredentialsProvider(options, endpointService);
         SetupFallbackErrorHandling(options, logger);

      services.AddSingleton(sp => botConfig);
    }

    private static void SetupFallbackErrorHandling(BotFrameworkOptions options, ILogger logger)
    {
      options.OnTurnError = async (context, exception) =>
      {
        logger.LogError($"Exception caught : {exception}");
        await context.SendActivityAsync(exception.ToString());
      };
    }

    private static void SetupCredentialsProvider(BotFrameworkOptions options, EndpointService endpointService)
    {
      options.CredentialProvider = new SimpleCredentialProvider(endpointService.AppId, endpointService.AppPassword);
    }

    private EndpointService GetEndpointService(BotConfiguration botConfig)
    {
      var environment = _isProduction ? "production" : "development";
      var service = botConfig.Services.FirstOrDefault(s => s.Type == "endpoint" && s.Name == environment);
      if (!(service is EndpointService endpointService))
      {
        throw new InvalidOperationException($"The .bot file does not contain an endpoint with name '{environment}'.");
      }

      return endpointService;
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      _loggerFactory = loggerFactory;

      app.UseDefaultFiles()
        .UseStaticFiles()
        .UseBotFramework();

    }
  }
}
