namespace RadicalCqsWebApp;

public interface ITodoItemsDaoOperationCallback
{
  void Success(Guid assignedId, string translation);
  void Error(int errorCode);
}