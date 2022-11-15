using ApplicationLogic.Ports;
using Microsoft.Net.Http.Headers;

namespace TodoApp1.Endpoints;

public class EndpointsAdapterRoot : IEndpointsRoot
{
    public EndpointsAdapterRoot(ITodoCommandFactory todoCommandFactory)
    {
        RetrieveTodoNoteEndpoint = new RetrieveTodoNoteEndpoint(todoCommandFactory);
        AddTodoEndpoint = 
          new HeaderValidatingEndpoint(HeaderNames.Accept, "application/json", 
            new AddTodoEndpoint(todoCommandFactory));
    }

    public IEndpoint RetrieveTodoNoteEndpoint { get; }
    public IEndpoint AddTodoEndpoint { get; }
}