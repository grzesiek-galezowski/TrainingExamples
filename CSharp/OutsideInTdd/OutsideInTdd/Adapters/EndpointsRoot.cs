using OutsideInTdd.App;

namespace OutsideInTdd.Adapters
{
    public class EndpointsRoot
    {
        private ITodoNoteDao NoteDao { get; }
        private readonly NoteDeserializer _noteDeserializer;
        public AddTodoEndpoint AddTodoEndpoint { get; }
        public GetAllTodosEndpoint AllTodosEndpoint { get; }

        public EndpointsRoot(TodoCommandFactory todoCommandFactory, ITodoNoteDao todoNoteDao)
        {
            NoteDao = todoNoteDao;
            _noteDeserializer = new NoteDeserializer();
            AddTodoEndpoint = new AddTodoEndpoint(
                _noteDeserializer, todoCommandFactory);
            AllTodosEndpoint = new GetAllTodosEndpoint(todoCommandFactory);

        }
    }
}