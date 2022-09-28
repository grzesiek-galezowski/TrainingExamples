using ApplicationLogic.Ports;

namespace ApplicationLogic;

public class AddTodoCommand : ITodoAppCommand
{
    private readonly IAddTodoResponseInProgress _addTodoResponseInProgress;
    private readonly NewTodoNoteDefinitionDto _newTodoNoteDefinitionDto;
    private readonly ITodoNoteDao _inMemoryTodoNoteDao;

    public AddTodoCommand(ITodoNoteDao inMemoryTodoNoteDao, NewTodoNoteDefinitionDto newTodoNoteDefinitionDto, IAddTodoResponseInProgress addTodoResponseInProgress)
    {
        _inMemoryTodoNoteDao = inMemoryTodoNoteDao;
        _newTodoNoteDefinitionDto = newTodoNoteDefinitionDto;
        _addTodoResponseInProgress = addTodoResponseInProgress;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
      var todoNoteMetadataDto = await _inMemoryTodoNoteDao.Save(
        _newTodoNoteDefinitionDto, 
        cancellationToken);
      await _addTodoResponseInProgress.Success(todoNoteMetadataDto, cancellationToken);
    }
}