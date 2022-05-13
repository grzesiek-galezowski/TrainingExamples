namespace ReturningResultsFromCommands;

public class AspNetCoreResponseFactory : IResponseFactory
{
    private readonly ILoggerFactory _loggerFactory;

    public AspNetCoreResponseFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public IAddTodoResponse ValidationFailed()
    {
        return new AddTodoValidationFailedResponse(
            _loggerFactory.CreateLogger<AddTodoValidationFailedResponse>(),
            "while validating a todo item");
    }

    public IAddTodoResponse Success(int id)
    {
        return new AddTodoSuccessResponse(id);
    }

    public IAddTodoResponse ApiCallFailed(ApiException apiException)
    {
        return new AddTodoApiCallFailedResponse(
            _loggerFactory.CreateLogger<AddTodoApiCallFailedResponse>(),
            apiException.Message);
    }
}