using Core.Either;

namespace ReturningResultsFromCommands;

public class TodoServiceReturningData
{
    private readonly ISomeKindOfApi _api;
    private readonly IValidation _validation;

    public TodoServiceReturningData(ISomeKindOfApi api, IValidation validation)
    {
        _api = api;
        _validation = validation;
    }

    public Either<AddTodoSuccess, AddTodoError> AddTodo(Todo todo)
    {
        if (!_validation.IsSuccessfulFor(todo))
        {
            return new AddTodoError(ErrorCode.ValidationFailed, "while validating a todo item");
        }

        try
        {
            var id = _api.Send(todo);
            return new AddTodoSuccess(id);
        }
        catch (ApiException e)
        {
            return new AddTodoError(ErrorCode.ApiCallFailed, $"Failed for {todo.Text}. Exception: {e}");
        }
    }
}

public interface ISomeKindOfApi
{
    int Send(Todo todo);
}