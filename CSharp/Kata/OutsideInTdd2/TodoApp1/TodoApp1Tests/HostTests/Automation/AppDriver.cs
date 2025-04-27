using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using TodoApp1;
using TodoApp1Tests.TestDtos;

namespace TodoApp1Tests.HostTests.Automation;

public class AppDriver : IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public static AppDriver CreateInstance()
    {
        return new AppDriver(new WebApplicationFactory<Program>()
          .WithWebHostBuilder(builder =>
          {
            builder.ConfigureAppConfiguration((context, configurationBuilder) =>
            {
              configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>()
              {
                ["Database:Path"] = Path.GetTempFileName()
              });
            });
          }));
    }

    private AppDriver(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    public void Start()
    {
        _ = FlurlClient;
    }

    public async Task<AttemptToCreateTodoNoteResponse> AttemptToCreateATodoNote(NewTodoNoteDefinitionTestDto dto)
    {
      return new AttemptToCreateTodoNoteResponse(
        await TodoEndpointAddRequest()
          .AllowAnyHttpStatus()
          .PostJsonAsync(dto));
    }

    public async ValueTask DisposeAsync()
    {
        await _webApplicationFactory.DisposeAsync();
    }

    public async Task<CreateTodoNoteResponse> CreateATodoNote(NewTodoNoteDefinitionTestDto dto)
    {
      var flurlResponse = await TodoEndpointAddRequest().PostJsonAsync(dto);
      var result = new CreateTodoNoteResponse(
        await flurlResponse.GetJsonAsync<TodoNoteMetadataTestDto>());
      return result;
    }

    public async Task<RetrieveTodoNoteResponse> RetrieveTodoNote(Guid noteId)
    {
      return new RetrieveTodoNoteResponse(
        await TodoEndpointRequest().AppendPathSegment(noteId)
        .GetJsonAsync<TodoNoteTestDto>());
    }

    private IFlurlRequest TodoEndpointRequest()
    {
      return FlurlClient
        .Request("/todo");
    }

    private IFlurlRequest TodoEndpointAddRequest()
    {
      return TodoEndpointRequest()
        .WithHeader(HeaderNames.Accept, "application/json");
    }

    private FlurlClient FlurlClient => new(_webApplicationFactory.CreateClient());
}