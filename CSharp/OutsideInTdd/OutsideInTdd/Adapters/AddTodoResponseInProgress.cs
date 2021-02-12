using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class AddTodoResponseInProgress : IAddTodoResponseInProgress
    {
        private readonly HttpContext _context;

        public AddTodoResponseInProgress(HttpContext context)
        {
            _context = context;
        }

        public async Task ReportFailureBecauseOfInappropriateWord(string inappropriateWord)
        {
            _context.Response.StatusCode = 400;
            await _context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(inappropriateWord));
        }
    }
}