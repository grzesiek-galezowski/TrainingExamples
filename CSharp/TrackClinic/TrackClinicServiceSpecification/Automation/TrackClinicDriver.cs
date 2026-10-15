using AwesomeAssertions;
using Flurl.Http;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;
using TrackClinic;

namespace TrackClinicServiceSpecification.Automation;

public class TrackClinicDriver : IAsyncDisposable
{
  private readonly string _databaseName;
  private readonly CancellationToken _cancellationToken;
  private readonly WebApp _webApp;
  private Lazy<FlurlClient> _client; //bug

  public TrackClinicDriver(CancellationToken cancellationToken)
  {
    _databaseName = Any.String("testDb");
    _cancellationToken = cancellationToken;
    _webApp = new WebApp(_databaseName);
    _client = new Lazy<FlurlClient>(() => new FlurlClient(_webApp.CreateClient()));
  }

  public async ValueTask DisposeAsync()
  {
    await _webApp.DisposeAsync();
    await SharedMongoInstance.DropDatabase(_databaseName, _cancellationToken);
  }

  public async Task Start()
  {
    _webApp.StartServer();
  }

  public async Task CreateSongProject(string artistName, CreateSongRequestDto createSongRequestDto)
  {
    var response = await _client.Value.Request($"/api/v1/artists/{artistName}/songs")
      .PostJsonAsync(createSongRequestDto, cancellationToken: _cancellationToken);
    response.StatusCode.Should().Be(200);
  }

  public async Task<RetrieveProjectDetailsResponse> RetrieveProjectDetails(string artist, string songName)
  {
    var response = await _client.Value.Request($"/api/v1/artists/{artist}/songs/{songName}")
      .GetAsync(cancellationToken: _cancellationToken);
    response.StatusCode.Should().Be(200);
    return new RetrieveProjectDetailsResponse(await response.GetJsonAsync<SongDetailsDto>());
  }

  public async Task<OpenApiDocumentResponse> GetOpenApiDocument()
  {
    var response = await _client.Value.Request("/openapi/v1.json")
      .GetAsync(cancellationToken: _cancellationToken);
    response.StatusCode.Should().Be(200);
    return new OpenApiDocumentResponse(await response.GetStringAsync());
  }
}