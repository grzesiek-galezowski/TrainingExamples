using System;
using ApplicationLogic;

internal class ConsoleRendering
{
  public void Show(ChangeLog[] changeLogs)
  {
    foreach (var changeLog in changeLogs)
    {
      Console.WriteLine(
        changeLog.PathOfLastVersion() + " => " +
        changeLog.ChangesCount() + ":" +
        changeLog.ComplexityOfLastVersion());
    }
  }
}