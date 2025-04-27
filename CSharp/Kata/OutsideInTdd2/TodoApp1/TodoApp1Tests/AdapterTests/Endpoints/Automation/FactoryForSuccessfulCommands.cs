using ApplicationLogic.Ports;
using Core.Maybe;

namespace TodoApp1Tests.AdapterTests.Endpoints.Automation;

internal class FactoryForSuccessfulCommands : ITodoCommandFactory
{
  private Maybe<TodoNoteDto> _dtoForRetrieval;

  public ITodoAppCommand CreateAddTodoCommand(
    NewTodoNoteDefinitionDto newTodoNoteDefinitionDto,
    IAddTodoResponseInProgress addTodoResponseInProgress)
  {
    return new ConfigurableCommand(async ct =>
    {
      await addTodoResponseInProgress.Success(new TodoNoteMetadataDto(Guid.NewGuid()), ct);
    });
  }

  public ITodoAppCommand CreateRetrieveTodoNoteCommand(Guid id, IGetTodoNoteResponseInProgress responseInProgress)
  {
    return new ConfigurableCommand(async ct =>
    {
      await responseInProgress.Success(
        _dtoForRetrieval.Value(),
        ct);
    });
  }

  //bug think this class through. Maybe it should be a stub, not a fake?
  public void RetrievingNoteReturns(TodoNoteDto returnedDto)
  {
    _dtoForRetrieval = returnedDto.Just();
  }
}