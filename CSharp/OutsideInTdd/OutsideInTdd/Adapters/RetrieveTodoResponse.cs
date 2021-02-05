using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class RetrieveTodoResponse : IRetrieveTodoResponse
    {
        private readonly HttpContext _context;

        public RetrieveTodoResponse(HttpContext context)
        {
            _context = context;
        }

        public Task ReportRetrievedData(List<TodoNoteDto> allItems)
        {
            return _context.Response.WriteAsJsonAsync(allItems);
        }
    }
}