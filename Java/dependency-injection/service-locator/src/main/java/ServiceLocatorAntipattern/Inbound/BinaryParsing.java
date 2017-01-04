package ServiceLocatorAntipattern.Inbound;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.InMessages.NullMessage;
import ServiceLocatorAntipattern.InMessages.StartMessage;
import ServiceLocatorAntipattern.InMessages.StopMessage;
import ServiceLocatorAntipattern.Interfaces.AcmeMessage;

public class BinaryParsing implements PacketParsing {
  public AcmeMessage resultFor(byte[] frameData) {
    if (frameData == null) {
      return ApplicationRoot.context.getComponent(NullMessage.class);
    } else if (frameData[0] == 1) {
      return ApplicationRoot.context.getComponent(StartMessage.class);
    } else {
      return ApplicationRoot.context.getComponent(StopMessage.class);
    }
  }
}