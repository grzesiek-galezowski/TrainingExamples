using MongoDB.Driver;
using ILogger = Serilog.ILogger;

namespace TrackClinic;

public class ServiceLogicRoot : IAsyncDisposable
{
  private readonly MongoClient _mongoClient;
  private readonly SongDao _songDao;

  public ServiceLogicRoot(string connectionString, ILogger logger)
  {
    _mongoClient = new MongoClient(connectionString);
    _songDao = new SongDao(_mongoClient, new MongoUrl(connectionString).DatabaseName);
  }

  public MongoClient MongoClient => _mongoClient;

  public SongDao SongDao => _songDao;

  public async ValueTask DisposeAsync()
  {
      _mongoClient.Dispose();
  }
}