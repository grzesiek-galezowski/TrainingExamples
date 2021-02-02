using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OutsideInTdd
{
    public class NoteParser
    {
        public async Task<TodoNoteDto> ReadNoteFrom(HttpRequest contextRequest)
        {
            return await contextRequest.ReadFromJsonAsync<TodoNoteDto>();
        }
    }
}