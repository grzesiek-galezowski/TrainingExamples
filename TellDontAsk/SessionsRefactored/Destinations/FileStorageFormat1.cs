using System;
using System.IO;

namespace SessionsRefactored.Destinations
{
  //Reuses the default order of dumping the session fields used by the BasicSession object
  //It wants session data always dumped in the default order
  public class FileStorageFormat1 : DumpDestination
  {
    private StreamWriter _writer;

    public void BeginNewSessionDump()
    {
      _writer = File.AppendText("zenek.txt");
    }

    public void AddId(int id)
    {
      _writer.WriteLine(id);
    }

    public void AddOwner(string owner)
    {
      _writer.WriteLine(owner);
    }

    public void AddTarget(string target)
    {
      _writer.WriteLine(target);
    }

    public void EndCurrentSessionDump()
    {
      _writer.Dispose();
      _writer = null;
    }
  }
}