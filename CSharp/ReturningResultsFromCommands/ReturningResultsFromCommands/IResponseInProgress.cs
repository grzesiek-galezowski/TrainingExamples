namespace ReturningResultsFromCommands;

public interface IResponseInProgress 
{
    void ValidationFailed();
    void Success(int id);
    void ApiCallFailed(ApiException apiException);
}

public interface IAspNetCoreResponseInProgress : IResponseInProgress
{
    public IResult ToAspNetCoreResult();
}

