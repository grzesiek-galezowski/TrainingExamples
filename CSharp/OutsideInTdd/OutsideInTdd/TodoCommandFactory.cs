namespace OutsideInTdd
{
    public class TodoCommandFactory
    {
        public TodoNoteDao _todoNoteDao;

        public TodoCommandFactory(TodoNoteDao todoNoteDao)
        {
            _todoNoteDao = todoNoteDao;
        }

        public AddNoteCommand CreateAddNoteCommand(TodoNoteDto dto)
        {
            return new AddNoteCommand(dto, _todoNoteDao);
        }

        public RetrieveAllNotesCommand CreateRetrieveAllNotesCommand(ITodoResponse todoResponse)
        {
            return new RetrieveAllNotesCommand(todoResponse, _todoNoteDao);
        }
    }
}