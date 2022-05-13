namespace ReturningResultsFromCommands;

public interface IAddTodoResponse
{
    IResult ToAspNetCoreResult();
}

public class AddTodoApiCallFailedResponse : IAddTodoResponse
{
    private readonly string _failureMessage;
    private readonly ILogger<AddTodoApiCallFailedResponse> _logger;

    public AddTodoApiCallFailedResponse(ILogger<AddTodoApiCallFailedResponse> logger, string failureMessage)
    {
        _logger = logger;
        _failureMessage = failureMessage;
    }

    public IResult ToAspNetCoreResult()
    {
        _logger.LogError($"Api call failed, {_failureMessage}");
        return Results.StatusCode(500);
    }
}

public class AddTodoValidationFailedResponse : IAddTodoResponse
{
    private readonly ILogger<AddTodoValidationFailedResponse> _logger;
    private readonly string _failureMessage;

    public AddTodoValidationFailedResponse(ILogger<AddTodoValidationFailedResponse> logger, string failureMessage)
    {
        _logger = logger;
        _failureMessage = failureMessage;
    }

    public IResult ToAspNetCoreResult()
    {
        _logger.LogError($"Validation failed, {_failureMessage}");
        return Results.BadRequest();
    }
}

public class AddTodoSuccessResponse : IAddTodoResponse
{
    private readonly int _id;

    public AddTodoSuccessResponse(int id)
    {
        _id = id;
    }

    public IResult ToAspNetCoreResult()
    {
        return Results.Ok(_id);
    }
}