using ApplicationLogic;
using ApplicationLogic.Ports;
using NSubstitute;

namespace ApplicationLogicTests.AppLogicTests.Automation;

public class AppLogicDriver
{
    private readonly ApplicationLogicRoot _applicationLogicRoot;
    private readonly FakeTodoNoteDao _todoNoteDao = new();

    public AppLogicDriver()
    {
        _applicationLogicRoot = new ApplicationLogicRoot(_todoNoteDao);
    }

    public void SetupNextDatabaseNoteId(Guid noteId)
    {
        _todoNoteDao.SetNextId(noteId);
    }

    public async Task<AddTodoNoteResponse> AddTodoNote(NewTodoNoteDefinitionDto newTodoNoteDefinitionDto)
    {
      var cancellationToken = new CancellationToken();
      var responseInProgress = Substitute.For<IAddTodoResponseInProgress>();
      await _applicationLogicRoot.TodoCommandFactory.CreateAddTodoCommand(
          newTodoNoteDefinitionDto,
          responseInProgress).Execute(cancellationToken);
      return new AddTodoNoteResponse(responseInProgress, cancellationToken);
    }

    public async Task<RetrieveTodoNoteResponse> RetrieveTodoNote(Guid noteId)
    {
        var getResponseInProgress = Substitute.For<IGetTodoNoteResponseInProgress>();
        var cancellationToken = new CancellationToken();
        await _applicationLogicRoot.TodoCommandFactory.CreateRetrieveTodoNoteCommand(
          noteId,
          getResponseInProgress)
          .Execute(cancellationToken);
        return new RetrieveTodoNoteResponse(getResponseInProgress, cancellationToken);
    }
}