namespace OutsideInTdd.App
{
    public class AddNoteCommand
    {
        private readonly ITodoNoteDao _storage;
        private readonly IAddTodoResponse _addTodoResponse;
        private readonly INewNoteRequest _newNoteRequest;

        public AddNoteCommand(
            INewNoteRequest newNoteRequest, 
            ITodoNoteDao storage, 
            IAddTodoResponse addTodoResponse)
        {
            _newNoteRequest = newNoteRequest;
            _storage = storage;
            _addTodoResponse = addTodoResponse;
        }

        public void Execute()
        {
            try
            {
                _newNoteRequest.AssertContainsOnlyAppropriateWords();
                _newNoteRequest.CreateNoteIn(_storage);
            }
            catch (InappropriateWordException e)
            {
                _addTodoResponse.ReportFailureBecauseOfInappropriateWord(e.Word);
            }
        } //bug catch generic exception as well!
    }
}