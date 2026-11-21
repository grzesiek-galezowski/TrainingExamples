using System.Diagnostics;

namespace DemoUi;

public class DemoUiActivity(Activity activity) : IDisposable
{
  public void RecordError(Exception ex)
  {
    activity.SetStatus(ActivityStatusCode.Error, ex.Message);
    activity.AddException(ex);
  }

  public void Dispose()
  {
    activity.Dispose();
  }
}