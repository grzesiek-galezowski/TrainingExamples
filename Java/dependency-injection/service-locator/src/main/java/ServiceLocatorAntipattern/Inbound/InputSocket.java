package ServiceLocatorAntipattern.Inbound;

public interface InputSocket {
  boolean receive(byte[] frameData);
}
