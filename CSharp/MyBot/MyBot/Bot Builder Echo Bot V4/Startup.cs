// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Functional.Maybe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static BotBuilderEchoBotV4.NullableExtensions;

namespace BotBuilderEchoBotV4
{
  public class Startup
  {
    private readonly bool _isProduction = false;
    private readonly IConfiguration _configuration;
    private Maybe<ILoggerFactory> _loggerFactory;

    public Startup(IHostingEnvironment env)
    {
      _isProduction = env.IsProduction();
      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();

      _configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      var botModule = new BotModule();
      var activityFactory = new ActivityFactory();
      var luisApplication = new LuisApplication("", "", "");
      var luisRecognizer = new LuisRecognizer(luisApplication);

      services.AddBot(
        ctx =>
        {
          return new GameStoreBot(
            activityFactory,
            new TurnContextPoweredObjectsFactory(
              botModule.CreateBotAccessors(),
              luisRecognizer));
        },
        options =>
        {
          botModule.Configure(
            options,
            _loggerFactory.Value,
            _isProduction,
            _configuration);
        });
    }

    // ReSharper disable once UnusedMember.Global
    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      _loggerFactory = loggerFactory.ToMaybe();

      app.UseBotFramework();
    }
  }
}
