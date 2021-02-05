using System.Collections.Generic;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class TodoNoteDao : ITodoNoteDao
    {
        public static TodoNoteDto? Dto;

        public void Save(TodoNoteDto dto)
        {
            Dto = dto;
        }

        public List<TodoNoteDto> LoadAllItems()
        {
            return new List<TodoNoteDto>()
            {
                Dto
            };
        }
    }
}