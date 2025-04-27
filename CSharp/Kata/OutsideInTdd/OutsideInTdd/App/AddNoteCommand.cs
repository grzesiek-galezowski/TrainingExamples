using System.Threading.Tasks;

namespace OutsideInTdd.App
{
    public class AddNoteCommand : ITodoCommand
    {
        private readonly ITodoNoteDao _storage;
        private readonly IAddTodoResponseInProgress _addTodoResponse;
        private readonly INewNoteRequest _newNoteRequest;

        public AddNoteCommand(
            INewNoteRequest newNoteRequest, 
            ITodoNoteDao storage, 
            IAddTodoResponseInProgress addTodoResponse)
        {
            _newNoteRequest = newNoteRequest;
            _storage = storage;
            _addTodoResponse = addTodoResponse;
        }

        public async Task Execute()
        {
            try
            {
                _newNoteRequest.AssertContainsOnlyAppropriateWords();
                _newNoteRequest.CreateNoteIn(_storage);
            }
            catch (InappropriateWordException e)
            {
                await _addTodoResponse.ReportFailureBecauseOfInappropriateWord(e.Word);
            }
        } //bug catch generic exception as well!
    }
}