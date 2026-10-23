using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace TrackClinicServiceSpecification.Automation;

public static class SharedMongoInstance
{
  private static readonly MongoDbContainer MongoContainer = new MongoDbBuilder("mongo:9.0")
    .Build();

  public static async Task Start()
  {
    await MongoContainer.StartAsync();
  }

  public static async Task DropDatabase(string databaseName, CancellationToken cancellationToken)
  {
    using var client = new MongoClient(MongoContainer.GetConnectionString());
    await client.DropDatabaseAsync(databaseName, cancellationToken);
  }

  public static async Task Stop()
  {
    await MongoContainer.DisposeAsync();
  }

  public static string GetConnectionString(string databaseName)
  {
    var baseConnectionString = MongoContainer.GetConnectionString();
    var mongoUrlBuilder = new MongoUrlBuilder(baseConnectionString)
    {
      DatabaseName = databaseName,
      AuthenticationSource = "admin"
    };

    return mongoUrlBuilder.ToString();
  }
}
