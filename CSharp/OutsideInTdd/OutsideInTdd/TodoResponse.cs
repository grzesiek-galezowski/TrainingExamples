using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OutsideInTdd
{
    public interface ITodoResponse
    {
        Task ReportRetrievedData(List<TodoNoteDto> allItems);
    }

    public class TodoResponse : ITodoResponse
    {
        private readonly HttpContext _context;

        public TodoResponse(HttpContext context)
        {
            _context = context;
        }

        public Task ReportRetrievedData(List<TodoNoteDto> allItems)
        {
            return _context.Response.WriteAsJsonAsync(allItems);
        }
    }
}