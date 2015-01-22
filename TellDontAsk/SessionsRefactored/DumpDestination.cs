using System;

namespace SessionsRefactored
{
  public interface DumpDestination
  {
    void BeginNewSessionDump();
    void AddDuration(TimeSpan duration);
    void AddOwner(string owner);
    void AddTarget(string target);
    void EndCurrentSessionDump();
  }
}