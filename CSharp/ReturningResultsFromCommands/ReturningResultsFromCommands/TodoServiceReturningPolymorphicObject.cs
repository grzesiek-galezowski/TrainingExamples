using Core.Either;

namespace ReturningResultsFromCommands;

public class TodoServiceReturningPolymorphicObject
{
    private readonly ISomeKindOfApi _api;
    private readonly IValidation _validation;
    private readonly IResponseFactory _responseFactory;

    public TodoServiceReturningPolymorphicObject(
        ISomeKindOfApi api, 
        IValidation validation,
        IResponseFactory responseFactory)
    {
        _api = api;
        _validation = validation;
        _responseFactory = responseFactory;
    }

    public IAddTodoResponse AddTodo(Todo todo)
    {
        if (!_validation.IsSuccessfulFor(todo))
        {
            return _responseFactory.ValidationFailed();
        }

        try
        {
            var id = _api.Send(todo);
            return _responseFactory.Success(id);
        }
        catch (ApiException e)
        {
            return _responseFactory.ApiCallFailed(e);
        }
    }
}