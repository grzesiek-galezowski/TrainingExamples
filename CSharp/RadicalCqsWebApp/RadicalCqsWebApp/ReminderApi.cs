namespace RadicalCqsWebApp;

public class ReminderApi
{
  public void RemindAbout(ISetReminderCallback callback, string title, DateTime dueDate)
  {
    if (title != null)
    {
      callback.Success(dueDate);
    }
    else 
    {
      callback.Error("retry limit expired");
    }
  }
}