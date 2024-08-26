namespace RadicalCqsWebApp;

public interface ISetReminderCallback
{
  void Success(DateTime dueDate, string translation);
  void Error(string message);
}