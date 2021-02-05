using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class NoteDeserializer
    {
        public async Task<TodoNoteDto> ReadNoteFrom(HttpRequest contextRequest)
        {
            return await contextRequest.ReadFromJsonAsync<TodoNoteDto>();
        }
    }
}