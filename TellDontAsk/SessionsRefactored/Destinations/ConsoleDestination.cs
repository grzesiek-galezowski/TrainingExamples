using System;

namespace SessionsRefactored.Destinations
{
  //Reuses the default order of dumping the session fields used by the BasicSession object
  //It wants session data always dumped in the default order
  public class ConsoleDestination : DumpDestination
  {
    public void BeginNewSessionDump()
    {
      Console.WriteLine("==> BEGIN SESSION");
    }

    public void AddId(int id)
    {
      Console.WriteLine(id);
    }

    public void AddOwner(string owner)
    {
      Console.WriteLine(owner);
    }

    public void AddTarget(string target)
    {
      Console.WriteLine(target);
    }

    public void EndCurrentSessionDump()
    {
      Console.WriteLine("==> END SESSION");
    }
  }
}