using System;
using System.IO;

namespace SessionsRefactored.Destinations
{
  // Shows that clients are still free to decide 
  // in what order they process dumped fields 
  // if they really, really want to.
  public class FileStorageFormat2 : DumpDestination
  {
    private TimeSpan? _duration;
    private string _owner;
    private string _target;

    public void BeginNewSessionDump()
    {
      _duration = null;
      _owner = null;
      _target = null;
    }

    public void AddDuration(TimeSpan duration)
    {
      _duration = duration;
    }

    public void AddOwner(string owner)
    {
      _owner = owner;
    }

    public void AddTarget(string target)
    {
      _target = target;
    }

    public void EndCurrentSessionDump()
    {
      using (var writer = File.AppendText("lolek.txt"))
      {
        //the original order was duration, order, target
        writer.WriteLine("<SESSION>");
        writer.WriteLine(_target ?? "No Target");
        writer.WriteLine(_owner ?? "No Owner");
        writer.WriteLine(_duration.HasValue ? _duration.Value.ToString() : "No duration");
        writer.WriteLine("</SESSION>");
      }
    }
  }
}