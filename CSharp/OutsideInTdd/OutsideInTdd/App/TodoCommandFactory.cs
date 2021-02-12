using OutsideInTdd.Adapters;

namespace OutsideInTdd.App
{
    public interface ITodoCommandFactory
    {
        ITodoCommand CreateAddNoteCommand(
            TodoNoteDto dto, 
            IAddTodoResponse addTodoResponse);

        RetrieveAllNotesCommand CreateRetrieveAllNotesCommand(
            IRetrieveTodoResponse retrieveTodoResponse);
    }

    public class TodoCommandFactory : ITodoCommandFactory
    {
        private readonly ITodoNoteDao _todoNoteDao;

        public TodoCommandFactory(ITodoNoteDao todoNoteDao)
        {
            _todoNoteDao = todoNoteDao;
        }

        public ITodoCommand CreateAddNoteCommand(
            TodoNoteDto dto, 
            IAddTodoResponse addTodoResponse)
        {
            return new AddNoteCommand(new NewNoteRequest(dto), _todoNoteDao, addTodoResponse);
        }

        public RetrieveAllNotesCommand CreateRetrieveAllNotesCommand(
            IRetrieveTodoResponse retrieveTodoResponse)
        {
            return new RetrieveAllNotesCommand(retrieveTodoResponse, _todoNoteDao);
        }
    }
}