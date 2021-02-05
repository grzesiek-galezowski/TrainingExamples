using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class GetAllTodosEndpoint : ITodoEndpoint
    {
        private readonly ITodoCommandFactory _todoCommandFactory;

        public GetAllTodosEndpoint(ITodoCommandFactory todoCommandFactory)
        {
            _todoCommandFactory = todoCommandFactory;
        }

        public Task HandleAsync(HttpContext context)
        {
            var todoResponse = new RetrieveTodoResponse(context);
            return _todoCommandFactory
                .CreateRetrieveAllNotesCommand(todoResponse).Execute();
        }
    }
}