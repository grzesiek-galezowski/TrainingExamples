using System.Threading.Tasks;

namespace OutsideInTdd
{
    public class RetrieveAllNotesCommand
    {
        private readonly TodoNoteDao _todoNoteDao;
        private readonly ITodoResponse _todoResponse;

        public RetrieveAllNotesCommand(ITodoResponse todoResponse, TodoNoteDao todoNoteDao)
        {
            _todoResponse = todoResponse;
            _todoNoteDao = todoNoteDao;
        }

        public Task Execute()
        {
            return _todoResponse.ReportRetrievedData(_todoNoteDao.LoadAllItems());
        }
    }
}