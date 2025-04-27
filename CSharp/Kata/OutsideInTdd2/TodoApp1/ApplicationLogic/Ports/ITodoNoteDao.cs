namespace ApplicationLogic.Ports;

public interface ITodoNoteDao
{
  Task<TodoNoteDto> ReadNoteById(Guid noteId, CancellationToken cancellationToken);
  Task<TodoNoteMetadataDto> Save(NewTodoNoteDefinitionDto newTodoNoteDefinitionDto, CancellationToken cancellationToken);
}