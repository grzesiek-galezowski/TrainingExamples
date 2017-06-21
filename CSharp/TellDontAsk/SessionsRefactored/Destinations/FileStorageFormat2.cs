using System;
using System.IO;

namespace SessionsRefactored.Destinations
{
  // Shows that clients are still free to decide 
  // in what order they process dumped fields 
  // if they really, really want to.
  public class FileStorageFormat2 : DumpDestination
  {
    private int? _id;
    private string _owner;
    private string _target;

    public void BeginNewSessionDump()
    {
      _id = null;
      _owner = null;
      _target = null;
    }

    public void AddId(int id)
    {
      _id = id;
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
        //the original order was id, order, target
        writer.WriteLine("<SESSION>");
        writer.WriteLine(_target ?? "No Target");
        writer.WriteLine(_owner ?? "No Owner");
        writer.WriteLine(_id.HasValue ? _id.Value.ToString() : "No ID!!");
        writer.WriteLine("</SESSION>");
      }
    }
  }
}