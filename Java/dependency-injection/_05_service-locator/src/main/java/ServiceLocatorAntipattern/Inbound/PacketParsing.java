package ServiceLocatorAntipattern.Inbound;

import ServiceLocatorAntipattern.Interfaces.Message;

public interface PacketParsing {
  Message resultFor(byte[] frameData);
}
