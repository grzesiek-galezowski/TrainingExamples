using ApplicationLogic.AddNewTodoNote;
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
          new NotifyRequesterOnSuccessfulNotePersistence(addTodoResponseInProgress),
          new NoteDefinitionByDto(newTodoNoteDefinitionDto));
    }

    public ITodoAppCommand CreateRetrieveTodoNoteCommand(Guid id, IGetTodoNoteResponseInProgress responseInProgress)
    {
        return new RetrieveTodoNoteCommand(
          id,
          responseInProgress,
          _inMemoryTodoNoteDao);
    }
}