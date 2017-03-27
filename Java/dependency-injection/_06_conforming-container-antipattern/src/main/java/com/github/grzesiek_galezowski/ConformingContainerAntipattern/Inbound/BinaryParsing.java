package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages.NullMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages.StartMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages.StopMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;

public class BinaryParsing implements PacketParsing {
  public InboundMessage resultFor(byte[] frameData) {
    if (frameData == null) {
      return ApplicationRoot.CONTEXT.resolve(NullMessage.class);
    } else if (frameData[0] == 1) {
      return ApplicationRoot.CONTEXT.resolve(StartMessage.class);
    } else {
      return ApplicationRoot.CONTEXT.resolve(StopMessage.class);
    }

  }
}
