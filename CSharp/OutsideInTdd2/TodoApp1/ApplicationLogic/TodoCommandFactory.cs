using ApplicationLogic.Ports;

namespace ApplicationLogic;

public class TodoCommandFactory : ITodoCommandFactory
{
    private readonly ITodoNoteDao _inMemoryTodoNoteDao;

    public TodoCommandFactory(ITodoNoteDao inMemoryTodoNoteDao)
    {
        _inMemoryTodoNoteDao = inMemoryTodoNoteDao;
    }

    public ITodoAppCommand CreateAddTodoCommand(NewTodoNoteDefinitionDto newTodoNoteDefinitionDto,
      IAddTodoResponseInProgress addTodoResponseInProgress)
    {
        return new AddTodoCommand(
          _inMemoryTodoNoteDao,
          newTodoNoteDefinitionDto,
          addTodoResponseInProgress);
    }

    public ITodoAppCommand CreateRetrieveTodoNoteCommand(Guid id, IGetTodoNoteResponseInProgress responseInProgress)
    {
        return new RetrieveTodoNoteCommand(
          id,
          responseInProgress,
          _inMemoryTodoNoteDao);
    }
}