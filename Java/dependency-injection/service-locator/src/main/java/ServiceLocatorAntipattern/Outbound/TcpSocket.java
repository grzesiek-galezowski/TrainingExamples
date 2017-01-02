package ServiceLocatorAntipattern.Outbound;

public class TcpSocket implements OutputSocket {
  public void open() {
    System.out.println("open");
  }

  public void close() {
    System.out.println("closing");
  }

  public void send(String content) {
    System.out.println(content);
  }
}
