package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;

public interface PacketParsing {
  InboundMessage resultFor(byte[] frameData);
}
