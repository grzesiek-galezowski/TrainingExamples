namespace RadicalCqsWebApp;

public interface ITodoItemsDaoOperationCallback
{
  void Success(Guid assignedId);
  void Error(int errorCode);
}