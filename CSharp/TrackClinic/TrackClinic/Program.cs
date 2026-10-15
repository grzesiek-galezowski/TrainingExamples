using MongoDB.Driver;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace TrackClinic;

public class Program
{
  public static async Task Main(string[] args)
  {
    Log.Logger = new LoggerConfiguration()
      .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
      .Enrich.FromLogContext()
      .WriteTo.Console(new JsonFormatter())
      .CreateBootstrapLogger();

    try
    {
      Log.Information("Starting web host");
      var builder = WebApplication.CreateBuilder(args);
      var appConfig = AppConfig.From(builder.Configuration);

      builder.Host.UseSerilog((context, services, configuration) => configuration
        .MinimumLevel.Is(appConfig.Logging.DefaultLevel)
        .MinimumLevel.Override("Microsoft", appConfig.Logging.MicrosoftOverride)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .ReadFrom.Services(services)
        .WriteTo.Console(new JsonFormatter()));
      //bug validation
      //bug local startup
      var connectionString = appConfig.DatabaseInfo.ConnectionString;
      using var mongoClient = new MongoClient(connectionString);
      var songs = new SongDao(mongoClient, new MongoUrl(connectionString).DatabaseName);

      // Add services to the container.
      builder.Services.AddAuthorization();

      // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
      builder.Services.AddOpenApi();
      builder.Services.AddHealthChecks().AddMongoDb(_ => mongoClient);

      var app = builder.Build();

      app.UseSerilogRequestLogging();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
      {
        //bug test for swagger
        app.MapOpenApi();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();
      //bug authorization (RBAC?)

      app.MapPost(
          "api/v1/artists/{artistName}/songs",
          async (string artistName, CreateSongRequestDto request) =>
          {
            await songs.SaveSong(artistName, request);
            Log.Information("Created song project for {ArtistName}: {SongTitle}", artistName, request.Title);
          })
        .WithName("CreateSongProject");
      app.MapGet(
          "api/v1/artists/{artistName}/songs/{songName}",
          async (string artistName, string songName) =>
          {
            return await songs.ReadSong(artistName, songName); //bug
          })
        .WithName("GetSongDetails");

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
}