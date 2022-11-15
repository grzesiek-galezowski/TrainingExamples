using ApplicationLogic.Ports;
using Core.Maybe;

namespace ApplicationLogicTests.AppLogicTests.Automation;

public class FakeTodoNoteDao : ITodoNoteDao
{
    private Guid _nextId = Guid.NewGuid();
    private Maybe<TodoNoteDto> _savedDto;

    public async Task<TodoNoteDto> ReadNoteById(Guid noteId, CancellationToken cancellationToken)
    {
        return _savedDto.Value();
    }

    public async Task<TodoNoteMetadataDto> Save(NewTodoNoteDefinitionDto newTodoNoteDefinitionDto, CancellationToken cancellationToken)
    {
      var currentId = _nextId;
      _nextId = Guid.NewGuid();
        _savedDto = new TodoNoteDto(
          newTodoNoteDefinitionDto.Title,
          newTodoNoteDefinitionDto.Content,
          currentId).Just();
        return new TodoNoteMetadataDto(currentId);
    }

    public void SetNextId(Guid nextId)
    {
        _nextId = nextId;
    }
}