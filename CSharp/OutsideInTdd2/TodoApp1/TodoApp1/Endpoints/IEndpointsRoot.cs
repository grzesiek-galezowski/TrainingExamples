namespace TodoApp1.Endpoints;

public interface IEndpointsRoot
{
    AddTodoEndpoint AddTodoEndpoint { get; }
    RetrieveTodoNoteEndpoint RetrieveTodoNoteEndpoint { get; }
}