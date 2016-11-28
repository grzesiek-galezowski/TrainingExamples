package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.InMessages.NullMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.InMessages.StartMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.InMessages.StopMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public class BinaryParsing implements IParsing {
  public AcmeMessage ResultFor(byte[] frameData) {
    if (frameData == null) {
      return new NullMessage();
    } else if (frameData[0] == 1) {
      return new StartMessage();
    } else {
      return new StopMessage();
    }
  }
}
