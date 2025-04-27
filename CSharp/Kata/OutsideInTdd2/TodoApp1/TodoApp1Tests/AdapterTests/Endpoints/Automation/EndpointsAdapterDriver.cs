using ApplicationLogic.Ports;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TodoApp1;
using TodoApp1.Endpoints;
using TodoApp1Tests.TestDtos;

namespace TodoApp1Tests.AdapterTests.Endpoints.Automation;

public class EndpointsAdapterDriver : IAsyncDisposable
{
  private readonly WebApplicationFactory<Program> _webApplicationFactory;
  private readonly FactoryForSuccessfulCommands _factoryForSuccessfulCommands;
  private FlurlClient FlurlClient => new(_webApplicationFactory.CreateClient());

  public EndpointsAdapterDriver()
  {
    _factoryForSuccessfulCommands = FactoryForSuccessfulCommands();
    _webApplicationFactory = new WebApplicationFactory<Program>()
      .WithWebHostBuilder(builder =>
      {
        builder.ConfigureTestServices(collection =>
        {
          collection.Replace(ServiceDescriptor.Singleton<IEndpointsRoot>(
            new EndpointsAdapterRoot(_factoryForSuccessfulCommands)));
        });
      });
  }

  private static FactoryForSuccessfulCommands FactoryForSuccessfulCommands()
  {
    return new FactoryForSuccessfulCommands();
  }

  public async Task<AddTodoItemResponse> AttemptToAddATodoItem(NewTodoNoteDefinitionTestDto dto)
  {
    return await AttemptToAddATodoItem(dto, r =>r);
  }

  public async Task<AddTodoItemResponse> AttemptToAddATodoItem(
    NewTodoNoteDefinitionTestDto dto, Func<AddTodoItemRequest, AddTodoItemRequest> customize)
  {
    var addTodoItemRequest = customize(new AddTodoItemRequest(TodoEndpoint()));
    return new AddTodoItemResponse(
      await addTodoItemRequest.AttemptToExecuteWith(dto));
  }

  public async Task<RetrieveTodoItemResponse> AttemptToRetrieveATodoItem(Guid id)
  {
    return new RetrieveTodoItemResponse(
      await TodoEndpoint().AppendPathSegment(id)
        .AllowAnyHttpStatus()
        .GetAsync());
  }

  public async ValueTask DisposeAsync()
  {
    await _webApplicationFactory.DisposeAsync();
  }


  public void RetrievingNoteFromAppLogicReturns(TodoNoteTestDto returnedDto)
  {
    _factoryForSuccessfulCommands.RetrievingNoteReturns(
      new TodoNoteDto(returnedDto.Title, returnedDto.Content, returnedDto.Id));
  }

  private IFlurlRequest TodoEndpoint()
  {
    return FlurlClient.Request("/todo");
  }
}