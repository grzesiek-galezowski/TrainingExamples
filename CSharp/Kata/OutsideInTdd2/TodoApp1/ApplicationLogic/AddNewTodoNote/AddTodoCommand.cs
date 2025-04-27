using ApplicationLogic.Ports;

namespace ApplicationLogic.AddNewTodoNote;

public class AddTodoCommand : ITodoAppCommand
{
  private readonly ITodoNoteDao _inMemoryTodoNoteDao;
  private readonly ITodoNoteDefinition _todoNoteDefinition;
  private readonly IAfterTodoNotePersistenceSteps _afterPersistenceSteps;

  public AddTodoCommand(
    ITodoNoteDao inMemoryTodoNoteDao,
    IAfterTodoNotePersistenceSteps afterPersistenceSteps,
    ITodoNoteDefinition todoNoteDefinition)
  {
    _inMemoryTodoNoteDao = inMemoryTodoNoteDao;
    _todoNoteDefinition = todoNoteDefinition;
    _afterPersistenceSteps = afterPersistenceSteps;
  }

  public async Task Execute(CancellationToken cancellationToken)
  {
    _todoNoteDefinition.Correct();
    await _todoNoteDefinition.PersistIn(
      _inMemoryTodoNoteDao,
      _afterPersistenceSteps,
      cancellationToken);
  }
}