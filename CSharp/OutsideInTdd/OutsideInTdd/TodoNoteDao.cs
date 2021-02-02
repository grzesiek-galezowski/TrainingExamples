using System.Collections.Generic;

namespace OutsideInTdd
{
    public class TodoNoteDao
    {
        public static TodoNoteDto? Dto;

        public TodoNoteDto Save(TodoNoteDto dto)
        {
            return Dto = dto;
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