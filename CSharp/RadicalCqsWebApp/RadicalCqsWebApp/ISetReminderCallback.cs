namespace RadicalCqsWebApp;

public interface ISetReminderCallback
{
  void Success(DateTime dueDate);
  void Error(string message);
}