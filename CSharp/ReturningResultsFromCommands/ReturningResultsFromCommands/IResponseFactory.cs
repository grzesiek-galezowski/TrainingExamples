namespace ReturningResultsFromCommands;

public interface IResponseFactory
{
    IAddTodoResponse ValidationFailed();
    IAddTodoResponse Success(int id);
    IAddTodoResponse ApiCallFailed(ApiException apiException);
}