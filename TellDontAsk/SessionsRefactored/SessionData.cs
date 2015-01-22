using System;

namespace SessionsRefactored
{
  public class SessionData
  {
    public string Owner { get; set; }
    public string Target { get; set; }
    public TimeSpan Duration { get; set; }
  }
}