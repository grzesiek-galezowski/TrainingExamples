using System;

namespace SessionsRefactored.Destinations
{
  // Demonstrates that the clients are still free to completely ignore
  // session contents, thus "disabling" the dump feature when it is part of bigger interaction
  // and is not needed
  public class DevNull : DumpDestination
  {
    public void BeginNewSessionDump()
    {
    }

    public void AddId(int id)
    {
    }

    public void AddOwner(string owner)
    {
    }

    public void AddTarget(string target)
    {
    }

    public void EndCurrentSessionDump()
    {
    }
  }
}