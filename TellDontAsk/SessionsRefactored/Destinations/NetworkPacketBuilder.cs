using System;
using System.Collections.Generic;
using SessionsRefactored.Network;

namespace SessionsRefactored.Destinations
{
  // Demonstrates that destination can accumulate sessions 
  // and a different piece of code can decide when and what to do with the messages
  // (see the SendBuiltPacketsThrough() method)
  public class NetworkPacketBuilder : DumpDestination
  {
    private SessionInformationMessage _message;
    private readonly List<SessionInformationMessage> _messages = new List<SessionInformationMessage>();

    public void BeginNewSessionDump()
    {
      _message = new SessionInformationMessage();
    }

    public void AddDuration(TimeSpan duration)
    {
      _message.Duration = duration;
    }

    public void AddOwner(string owner)
    {
      _message.Owner = owner;
    }

    public void AddTarget(string target)
    {
      _message.Target = target;
    }

    public void EndCurrentSessionDump()
    {
      _messages.Add(_message);
    }


    public void SendBuiltPacketsThrough(NetworkConnection networkConnection)
    {
      networkConnection.Send(_messages);
    }
  }
}