using ApplicationLogic.Ports;

namespace TodoApp1.Endpoints;

public class AddTodoResponseInProgress : IAddTodoResponseInProgress
{
    private readonly HttpContext _context;

    public AddTodoResponseInProgress(HttpContext context)
    {
        _context = context;
    }

    public async Task Success(TodoNoteMetadataDto todoNoteMetadataDto, CancellationToken cancellationToken)
    {
        await Results.Ok(todoNoteMetadataDto).ExecuteAsync(_context);
    }
}