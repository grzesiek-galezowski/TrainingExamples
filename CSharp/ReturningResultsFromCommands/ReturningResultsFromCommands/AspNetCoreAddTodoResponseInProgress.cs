using Core.Maybe;

namespace ReturningResultsFromCommands;

public class AspNetCoreAddTodoResponseInProgress : IAspNetCoreResponseInProgress
{
    private readonly ILogger<AspNetCoreAddTodoResponseInProgress> _logger;
    private Maybe<IResult> _response;

    public AspNetCoreAddTodoResponseInProgress(ILogger<AspNetCoreAddTodoResponseInProgress> logger)
    {
        _logger = logger;
    }

    public void ValidationFailed()
    {
        _logger.LogError("Validation failed");
        _response = Results.BadRequest().Just();
    }

    public void Success(int id)
    {
        _response = Results.Ok(id).Just();
    }

    public void ApiCallFailed(ApiException apiException)
    {
        _logger.LogError($"Api call failed, {apiException.Message}");
        _response = Results.StatusCode(500).Just();
    }

    public IResult ToAspNetCoreResult()
    {
        return _response.Value();
    }
}