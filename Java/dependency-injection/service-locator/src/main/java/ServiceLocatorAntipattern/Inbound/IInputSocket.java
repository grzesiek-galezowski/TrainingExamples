package ServiceLocatorAntipattern.Inbound;

public interface IInputSocket {
  boolean receive(byte[] frameData);
}
