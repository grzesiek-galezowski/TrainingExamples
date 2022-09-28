using ApplicationLogic.Ports;

namespace TodoApp1.Endpoints;

public class EndpointsAdapterRoot : IEndpointsRoot
{
    public EndpointsAdapterRoot(ITodoCommandFactory todoCommandFactory)
    {
        RetrieveTodoNoteEndpoint = new RetrieveTodoNoteEndpoint(todoCommandFactory);
        AddTodoEndpoint = new AddTodoEndpoint(todoCommandFactory);
    }

    public RetrieveTodoNoteEndpoint RetrieveTodoNoteEndpoint { get; }
    public AddTodoEndpoint AddTodoEndpoint { get; }
}