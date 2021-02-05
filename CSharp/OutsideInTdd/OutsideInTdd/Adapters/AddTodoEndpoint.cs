using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class AddTodoEndpoint : ITodoEndpoint
    {
        private readonly NoteDeserializer _noteDeserializer;
        private readonly ITodoCommandFactory _todoCommandFactory;

        public AddTodoEndpoint(
            NoteDeserializer noteDeserializer, 
            ITodoCommandFactory todoCommandFactory)
        {
            _noteDeserializer = noteDeserializer;
            _todoCommandFactory = todoCommandFactory;
        }

        public async Task HandleAsync(HttpContext context)
        {
            var dto = await _noteDeserializer.ReadNoteFrom(context.Request);
            _todoCommandFactory.CreateAddNoteCommand(dto,
                new AddTodoResponse(context)).Execute();
        }
    }
}