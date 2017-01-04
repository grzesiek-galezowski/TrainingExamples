package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;

public interface Outbound {
  void send(InboundMessage message);
}
