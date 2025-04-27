namespace ApplicationLogic.Ports;

public interface IGetTodoNoteResponseInProgress
{
    Task Success(TodoNoteDto note, CancellationToken cancellationToken);
}