using ApplicationLogic;
using TodoApp1.Database;
using TodoApp1.Endpoints;

namespace TodoApp1.Bootstrap;

public class ServiceLogicRoot : IEndpointsRoot
{
    private readonly EndpointsAdapterRoot _endpointsAdapterRoot;

    public ServiceLogicRoot(DatabaseOptions databaseOptions)
    {
        var databaseAdapterRoot = new DatabaseAdapterRoot(databaseOptions.Path);
        var applicationLogicRoot = new ApplicationLogicRoot(databaseAdapterRoot.TodoNoteDao);
        _endpointsAdapterRoot = new EndpointsAdapterRoot(applicationLogicRoot.TodoCommandFactory);
    }

    public IEndpoint AddTodoEndpoint =>
      _endpointsAdapterRoot.AddTodoEndpoint;

    public IEndpoint RetrieveTodoNoteEndpoint =>
      _endpointsAdapterRoot.RetrieveTodoNoteEndpoint;
}