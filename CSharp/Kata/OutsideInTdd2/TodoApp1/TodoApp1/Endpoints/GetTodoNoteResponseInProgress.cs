using ApplicationLogic.Ports;

namespace TodoApp1.Endpoints;

public class GetTodoNoteResponseInProgress : IGetTodoNoteResponseInProgress
{
    private readonly HttpContext _context;

    public GetTodoNoteResponseInProgress(HttpContext context)
    {
        _context = context;
    }

    public async Task Success(TodoNoteDto note, CancellationToken cancellationToken)
    {
        await Results.Ok(note).ExecuteAsync(_context);
    }
}