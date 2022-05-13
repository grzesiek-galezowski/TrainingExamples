namespace ReturningResultsFromCommands;

public class TodoServiceAcceptingPolymorphicObject
{
    private readonly ISomeKindOfApi _api;
    private readonly IValidation _validation;

    public TodoServiceAcceptingPolymorphicObject(
        ISomeKindOfApi api, 
        IValidation validation)
    {
        _api = api;
        _validation = validation;
    }

    public void AddTodo(Todo todo, IResponseInProgress responseInProgress)
    {
        if (!_validation.IsSuccessfulFor(todo))
        {
            responseInProgress.ValidationFailed();
        }
        else
        {
            try
            {
                var id = _api.Send(todo);
                responseInProgress.Success(id);
            }
            catch (ApiException e)
            {
                responseInProgress.ApiCallFailed(e);
            }
        }
    }
}