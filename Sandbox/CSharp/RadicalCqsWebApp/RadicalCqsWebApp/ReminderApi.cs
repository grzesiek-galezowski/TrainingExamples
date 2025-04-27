namespace RadicalCqsWebApp;

public class ReminderApi
{
  public void RemindAbout(ISetReminderCallback callback, string title, DateTime dueDate, string translation)
  {
    if (title != null)
    {
      callback.Success(dueDate, translation);
    }
    else 
    {
      callback.Error("retry limit expired");
    }
  }
}