using ApplicationLogic.Ports;

namespace ApplicationLogic.AddNewTodoNote;

public interface ITodoNoteDefinition
{
    Task PersistIn(ITodoNoteDao inMemoryTodoNoteDao,
      IAfterTodoNotePersistenceSteps afterPersistenceSteps,
      CancellationToken cancellationToken);

    void Correct();
}

public class NoteDefinitionByDto : ITodoNoteDefinition
{
    private NewTodoNoteDefinitionDto _newTodoNoteDefinitionDto;

    public NoteDefinitionByDto(NewTodoNoteDefinitionDto newTodoNoteDefinitionDto)
    {
        _newTodoNoteDefinitionDto = newTodoNoteDefinitionDto;
    }

    public async Task PersistIn(
      ITodoNoteDao inMemoryTodoNoteDao,
      IAfterTodoNotePersistenceSteps afterPersistenceSteps,
      CancellationToken cancellationToken)
    {
        var todoNoteMetadataDto = await inMemoryTodoNoteDao.Save(
          _newTodoNoteDefinitionDto,
          cancellationToken);
        await afterPersistenceSteps.ExecuteFor(todoNoteMetadataDto, cancellationToken);
    }

    public void Correct()
    {
      _newTodoNoteDefinitionDto = _newTodoNoteDefinitionDto with
      {
        Content = _newTodoNoteDefinitionDto.Content.Replace("truck", "duck")
      };
    }
}