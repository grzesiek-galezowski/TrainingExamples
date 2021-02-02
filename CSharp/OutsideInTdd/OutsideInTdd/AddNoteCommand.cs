namespace OutsideInTdd
{
    public class AddNoteCommand
    {
        public TodoNoteDao _todoNoteDao;
        public TodoNoteDto _dto;

        public AddNoteCommand(TodoNoteDto dto, TodoNoteDao todoNoteDao)
        {
            _dto = dto;
            _todoNoteDao = todoNoteDao;
        }

        public void Execute()
        {
            _todoNoteDao.Save(_dto);
        }
    }
}