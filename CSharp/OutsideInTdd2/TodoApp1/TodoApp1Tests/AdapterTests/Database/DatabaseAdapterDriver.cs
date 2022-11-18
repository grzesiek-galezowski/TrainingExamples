using ApplicationLogic.Ports;
using TodoApp1.Database;

namespace TodoApp1Tests.AdapterTests.Database;

public class DatabaseAdapterDriver
{
    private readonly DatabaseAdapterRoot _databaseAdapterRoot;
    public string FilePath { get; }

    private DatabaseAdapterDriver(string filePath)
    {
        FilePath = filePath;
        _databaseAdapterRoot =
          new DatabaseAdapterRoot(
            FilePath);
    }

    public static DatabaseAdapterDriver Default()
    {
      return new DatabaseAdapterDriver(Path.GetTempFileName());
    }

    public static DatabaseAdapterDriver WithPath(string filePath)
    {
        return new DatabaseAdapterDriver(filePath);
    }

    public async Task<TodoNoteMetadataDto> Save(NewTodoNoteDefinitionDto noteDefinition)
    {
        return await _databaseAdapterRoot.TodoNoteDao
          .Save(noteDefinition, new CancellationToken());
    }

    public async Task<TodoNoteDto> ReadNoteById(Guid noteId)
    {
        return await _databaseAdapterRoot.TodoNoteDao.ReadNoteById(
          noteId,
          new CancellationToken());
    }
}