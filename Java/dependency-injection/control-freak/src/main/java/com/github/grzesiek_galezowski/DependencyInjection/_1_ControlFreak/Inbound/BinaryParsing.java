package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Inbound;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.InMessages.NullMessage;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.InMessages.StartMessage;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.InMessages.StopMessage;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;

class BinaryParsing {
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
