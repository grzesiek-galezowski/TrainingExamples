package com.github.grzesiek_galezowski.BastardInjection.Inbound;

import com.github.grzesiek_galezowski.BastardInjection.InMessages.NullMessage;
import com.github.grzesiek_galezowski.BastardInjection.InMessages.StartMessage;
import com.github.grzesiek_galezowski.BastardInjection.InMessages.StopMessage;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;

class BinaryParsing implements IPacketParsing {
  public AcmeMessage resultFor(byte[] frameData) {
    if (frameData == null) {
      return new NullMessage();
    } else if (frameData[0] == 1) {
      return new StartMessage();
    } else {
      return new StopMessage();
    }

  }
}
