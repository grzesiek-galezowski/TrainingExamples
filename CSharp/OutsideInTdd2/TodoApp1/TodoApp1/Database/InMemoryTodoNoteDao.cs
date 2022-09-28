using System.Text.Json;
using ApplicationLogic.Ports;
using Core.NullableReferenceTypesExtensions;

namespace TodoApp1.Database;

public class InMemoryTodoNoteDao : ITodoNoteDao
{
  private readonly string _filePath;

  public InMemoryTodoNoteDao(string filePath)
  {
    _filePath = filePath;
  }

  public async Task<TodoNoteDto> ReadNoteById(Guid noteId, CancellationToken cancellationToken)
  {
    var notes = await ReadTodoNoteDtos(cancellationToken);
    return notes.First(note => note.Id == noteId);
  }

  public async Task<TodoNoteMetadataDto> Save(
    NewTodoNoteDefinitionDto newTodoNoteDefinitionDto,
    CancellationToken cancellationToken)
  {
    var newGuid = Guid.NewGuid();
    var todoNoteDto = new TodoNoteDto(newTodoNoteDefinitionDto.Title, newTodoNoteDefinitionDto.Content, newGuid);
    var notes = (await ReadTodoNoteDtos(cancellationToken)).Append(todoNoteDto);
    await Save(notes, cancellationToken);
    return new TodoNoteMetadataDto(newGuid);
  }

  private async Task Save(IEnumerable<TodoNoteDto> notes, CancellationToken cancellationToken)
  {
    var serializedDto = JsonSerializer.Serialize(notes);
    await File.WriteAllTextAsync(_filePath, serializedDto, cancellationToken);
  }

  private async Task<TodoNoteDto[]> ReadTodoNoteDtos(CancellationToken cancellationToken)
  {
    var fileText = await File.ReadAllTextAsync(_filePath, cancellationToken);
    if (fileText.Length == 0)
    {
      return new TodoNoteDto[] { };
    }
    var notes = JsonSerializer.Deserialize<TodoNoteDto[]>(fileText).OrThrow();
    return notes;
  }
}