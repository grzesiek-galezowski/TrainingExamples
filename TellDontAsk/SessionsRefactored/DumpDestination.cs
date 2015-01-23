using System;

namespace SessionsRefactored
{
  public interface DumpDestination
  {
    void BeginNewSessionDump();
    void AddId(int id);
    void AddOwner(string owner);
    void AddTarget(string target);
    void EndCurrentSessionDump();
  }
}