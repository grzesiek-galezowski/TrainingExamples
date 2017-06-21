using System.Collections.Generic;
using SessionsRefactored.Destinations;

namespace SessionsRefactored.Network
{
  public class BogusNetworkConnection : NetworkConnection
  {
    public void Send(List<SessionInformationMessage> messages)
    {
      //bogus
    }
  }
}