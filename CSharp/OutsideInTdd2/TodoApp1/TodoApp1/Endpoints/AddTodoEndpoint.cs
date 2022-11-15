using ApplicationLogic.Ports;
using Core.NullableReferenceTypesExtensions;

namespace TodoApp1.Endpoints;

public class AddTodoEndpoint : IEndpoint
{
    private readonly ITodoCommandFactory _todoCommandFactory;

    public AddTodoEndpoint(ITodoCommandFactory todoCommandFactory)
    {
        _todoCommandFactory = todoCommandFactory;
    }

    public async Task Handle(HttpContext context)
    {
        var newTodoNoteDefinitionDto =
          (await context.Request.ReadFromJsonAsync<NewTodoNoteDefinitionDto>())
          .OrThrow(); //bug better validation later
        var addTodoResponseInProgress = new AddTodoResponseInProgress(context);


        var addTodoCommand = _todoCommandFactory
          .CreateAddTodoCommand(newTodoNoteDefinitionDto, addTodoResponseInProgress);
        await addTodoCommand.Execute(context.RequestAborted);
    }
}