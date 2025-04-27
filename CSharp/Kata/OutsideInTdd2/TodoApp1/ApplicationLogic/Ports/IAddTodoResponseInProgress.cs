namespace ApplicationLogic.Ports;

public interface IAddTodoResponseInProgress
{
    Task Success(TodoNoteMetadataDto todoNoteMetadataDto, CancellationToken cancellationToken);
}