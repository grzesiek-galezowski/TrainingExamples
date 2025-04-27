using ApplicationLogic.Ports;
using NSubstitute;

namespace ApplicationLogicTests.AppLogicTests.Automation;

public class AddTodoNoteResponse
{
  private readonly IAddTodoResponseInProgress _responseInProgress;
  private readonly CancellationToken _cancellationToken;

  public AddTodoNoteResponse(
    IAddTodoResponseInProgress responseInProgress,
    CancellationToken cancellationToken)
  {
    _responseInProgress = responseInProgress;
    _cancellationToken = cancellationToken;
  }

  public async Task ShouldContain(Guid noteId)
  {
    await _responseInProgress.Received(1).Success(
      new TodoNoteMetadataDto(noteId),
      _cancellationToken);
  }
}