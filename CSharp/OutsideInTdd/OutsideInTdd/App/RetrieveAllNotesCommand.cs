using System.Threading.Tasks;

namespace OutsideInTdd.App
{
    public class RetrieveAllNotesCommand
    {
        private readonly ITodoNoteDao _todoNoteDao;
        private readonly IRetrieveTodoResponse _retrieveTodoResponse;

        public RetrieveAllNotesCommand(
            IRetrieveTodoResponse retrieveTodoResponse,
            ITodoNoteDao todoNoteDao)
        {
            _retrieveTodoResponse = retrieveTodoResponse;
            _todoNoteDao = todoNoteDao;
        }

        public Task Execute()
        {
            return _retrieveTodoResponse.ReportRetrievedData(_todoNoteDao.LoadAllItems());
        }
    }
}