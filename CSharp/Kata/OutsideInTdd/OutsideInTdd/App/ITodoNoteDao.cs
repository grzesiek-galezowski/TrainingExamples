using System.Collections.Generic;

namespace OutsideInTdd.App
{
    public interface ITodoNoteDao
    {
        void Save(TodoNoteDto dto);
        List<TodoNoteDto> LoadAllItems();
    }
}