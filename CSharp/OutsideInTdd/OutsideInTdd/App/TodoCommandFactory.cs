using OutsideInTdd.Adapters;

namespace OutsideInTdd.App
{
    public class TodoCommandFactory
    {
        private readonly ITodoNoteDao _todoNoteDao;

        public TodoCommandFactory(ITodoNoteDao todoNoteDao)
        {
            _todoNoteDao = todoNoteDao;
        }

        public AddNoteCommand CreateAddNoteCommand(
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