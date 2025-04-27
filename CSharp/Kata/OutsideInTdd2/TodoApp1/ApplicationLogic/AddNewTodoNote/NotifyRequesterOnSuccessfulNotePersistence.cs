using ApplicationLogic.Ports;

namespace ApplicationLogic.AddNewTodoNote;

public interface IAfterTodoNotePersistenceSteps
{
    Task ExecuteFor(
      TodoNoteMetadataDto todoNoteMetadataDto,
      CancellationToken cancellationToken);
}

public class NotifyRequesterOnSuccessfulNotePersistence : IAfterTodoNotePersistenceSteps
{
    private readonly IAddTodoResponseInProgress _addTodoResponseInProgress;

    public NotifyRequesterOnSuccessfulNotePersistence(
      IAddTodoResponseInProgress addTodoResponseInProgress)
    {
        _addTodoResponseInProgress = addTodoResponseInProgress;
    }

    public async Task ExecuteFor(
      TodoNoteMetadataDto todoNoteMetadataDto,
      CancellationToken cancellationToken)
    {
        await _addTodoResponseInProgress.Success(todoNoteMetadataDto, cancellationToken);
    }
}