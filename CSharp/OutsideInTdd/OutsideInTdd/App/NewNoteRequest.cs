namespace OutsideInTdd.App
{
    public interface INewNoteRequest
    {
        void CreateNoteIn(ITodoNoteDao todoNoteDao);
        void AssertContainsOnlyAppropriateWords();
    }

    public class NewNoteRequest : INewNoteRequest
    {
        private readonly TodoNoteDto _dto; //bug make private

        public NewNoteRequest(TodoNoteDto dto)
        {
            _dto = dto;
        }

        public void CreateNoteIn(ITodoNoteDao todoNoteDao)
        {
            todoNoteDao.Save(_dto);
        }

        public void AssertContainsOnlyAppropriateWords()
        {
            if (_dto.Content.Contains("Banan"))
            {
                throw new InappropriateWordException("Banan");
            }
            //bug should have added UTs
        }
    }
}