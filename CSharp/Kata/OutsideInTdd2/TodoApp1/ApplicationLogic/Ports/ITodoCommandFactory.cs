namespace ApplicationLogic.Ports;

public interface ITodoCommandFactory
{
  ITodoAppCommand CreateAddTodoCommand(
    NewTodoNoteDefinitionDto newTodoNoteDefinitionDto,
    IAddTodoResponseInProgress addTodoResponseInProgress);

  ITodoAppCommand CreateRetrieveTodoNoteCommand(
    Guid id,
    IGetTodoNoteResponseInProgress responseInProgress);

}