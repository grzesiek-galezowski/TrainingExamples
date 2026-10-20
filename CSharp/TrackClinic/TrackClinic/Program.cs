using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi;
using Core.NullableReferenceTypesExtensions;
using Scalar.AspNetCore;
using Serilog;
using ILogger = Serilog.ILogger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using Core.Maybe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;

namespace TrackClinic;

public class CreateSongProjectEndpoint(ServiceLogicRoot serviceLogicRoot, string artistName, CreateSongRequestDto request)
{
  public async Task Handle(CancellationToken cancellationToken)
  {
    await serviceLogicRoot.SongDao.SaveSong(artistName, request, cancellationToken);
    Log.Information("Created song project for {ArtistName}: {SongTitle}", artistName, request.Title);
  }
}

public class Program
{
  public static async Task Main(string[] args)
  {
    Log.Logger = LoggingConfigurations.ConfigureBootstrapLogger(new LoggerConfiguration());

    try
    {
      Log.Information("Starting web host");
      var builder = WebApplication.CreateBuilder(args);
      var appConfig = AppConfig.From(builder.Configuration);

      builder.Host.UseSerilog((_, services, configuration) => LoggingConfigurations.ConfigureServiceLogging(
        configuration,
        services,
        appConfig.Logging.DefaultLevel,
        appConfig.Logging.MicrosoftOverride));
      //bug logging tests
      //bug config reading tests
      var connectionString = appConfig.DatabaseInfo.ConnectionString.OrThrow();

      builder.Services.AddSingleton<ServiceLogicRoot>(ctx => new ServiceLogicRoot(
        connectionString,
        ctx.GetRequiredService<ILogger>()));

      builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => { AuthorizationConfiguration.ConfigureBearerTokenHandling(options, appConfig); });
      builder.Services.AddAuthorization(AuthorizationConfiguration.ConfigureRoles);

      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      builder.Services.AddOpenApi(OpenApiConfiguration.ConfigureOpenApiDocument);
      builder.Services.AddHealthChecks().AddMongoDb(
        ctx => ctx.GetRequiredService<ServiceLogicRoot>().MongoClient,
        tags: ["ready"]);

      var app = builder.Build();
      var serviceLogicRoot = app.Services.GetRequiredService<ServiceLogicRoot>();
      app.UseSerilogRequestLogging();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        app.MapOpenApi();
        app.MapScalarApiReference();
      }

      app.UseHttpsRedirection();

      app.UseAuthentication();
      app.UseAuthorization();
      app.MapHealthChecks(
        "/health",
        new HealthCheckOptions
        {
          Predicate = _ => false //bug what is this?
        });
      app.MapHealthChecks(
        "/ready",
        new HealthCheckOptions
        {
          Predicate = check => check.Tags.Contains("ready")
        });

      app.MapPost(
          "api/v1/artists/{artistName}/songs",
          async (string artistName, CreateSongRequestDto request, CancellationToken cancellationToken) =>
          {
            await new CreateSongProjectEndpoint(serviceLogicRoot, artistName, request).Handle(cancellationToken);
            return Results.Created($"/api/v1/artists/{artistName}/songs/{request.Title}", null);
          })
        .WithName("CreateSongProject")
        .WithTags("Songs")
        .WithSummary("Creates a new song project")
        .WithDescription("Initializes a new song project for the specified artist with the given title.")
        .RequireAuthorization(AuthorizationConfiguration.DefaultPolicyName)
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

      app.MapGet(
          "api/v1/artists/{artistName}/songs/{songName}",
          async (string artistName, string songName, CancellationToken cancellationToken) =>
          {
            //bug continue the refactoring to move the logic into a separate endpoint class
            var songs = serviceLogicRoot.SongDao;
            var song = await songs.ReadSong(artistName, songName, cancellationToken);
            return song is not null ? Results.Ok(song) : Results.NotFound();
          })
        .WithName("GetSongDetails")
        .WithTags("Songs")
        .WithSummary("Gets details of a song project")
        .WithDescription("Retrieves the full details of a specific song project, including milestones and timeline.")
        .RequireAuthorization(AuthorizationConfiguration.DefaultPolicyName)
        .Produces<SongDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

      await app.RunAsync();
    }
    catch (Exception ex)
    {
      Log.Fatal(ex, "Host terminated unexpectedly");
    }
    finally
    {
      await Log.CloseAndFlushAsync();
    }
  }

  //bug move?
  //bug metrics
}