package ServiceLocatorAntipattern.Inbound;

import ServiceLocatorAntipattern.Interfaces.AcmeMessage;

public interface PacketParsing {
  AcmeMessage resultFor(byte[] frameData);
}
