namespace TodoApp1.Endpoints;

public interface IEndpointsRoot
{
    IEndpoint AddTodoEndpoint { get; }
    IEndpoint RetrieveTodoNoteEndpoint { get; }
}