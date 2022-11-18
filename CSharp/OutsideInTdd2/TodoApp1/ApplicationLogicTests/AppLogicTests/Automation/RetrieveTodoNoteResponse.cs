using ApplicationLogic.Ports;
using NSubstitute;

namespace ApplicationLogicTests.AppLogicTests.Automation;

public class RetrieveTodoNoteResponse
{
    private readonly IGetTodoNoteResponseInProgress _responseInProgress;
    private readonly CancellationToken _cancellationToken;

    public RetrieveTodoNoteResponse(
      IGetTodoNoteResponseInProgress responseInProgress,
      CancellationToken cancellationToken)
    {
      _responseInProgress = responseInProgress;
      _cancellationToken = cancellationToken;
    }

    public async Task ShouldContainNoteBasedOn(NewTodoNoteDefinitionDto newTodoNoteDefinitionDto, Guid noteId)
    {
        await _responseInProgress.Received(1).Success(new TodoNoteDto(
          newTodoNoteDefinitionDto.Title,
          newTodoNoteDefinitionDto.Content,
          noteId),
          _cancellationToken);
    }
}