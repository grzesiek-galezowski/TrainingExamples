using ApplicationLogic.Ports;

namespace ApplicationLogic;

public class RetrieveTodoNoteCommand : ITodoAppCommand
{
    private readonly IGetTodoNoteResponseInProgress _responseInProgress;
    private readonly Guid _id;
    private readonly ITodoNoteDao _inMemoryTodoNoteDao;

    public RetrieveTodoNoteCommand(
      Guid id,
      IGetTodoNoteResponseInProgress responseInProgress,
      ITodoNoteDao inMemoryTodoNoteDao)
    {
        _id = id;
        _responseInProgress = responseInProgress;
        _inMemoryTodoNoteDao = inMemoryTodoNoteDao;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        var note = await _inMemoryTodoNoteDao.ReadNoteById(_id, cancellationToken);
        await _responseInProgress.Success(note, cancellationToken);
    }
}