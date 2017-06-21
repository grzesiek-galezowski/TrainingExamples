using System.Collections.Generic;
using SessionsRefactored.Network;

namespace SessionsRefactored.Destinations
{
  public interface NetworkConnection
  {
    void Send(List<SessionInformationMessage> messages);
  }
}