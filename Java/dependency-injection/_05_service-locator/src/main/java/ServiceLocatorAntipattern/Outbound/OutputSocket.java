package ServiceLocatorAntipattern.Outbound;

public interface OutputSocket {
  void open();

  void close();

  void send(String content);
}
