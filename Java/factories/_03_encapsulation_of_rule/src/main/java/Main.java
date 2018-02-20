public class Main {
  public static void main() {
    MySystem system = new MySystem(
        new MessageDispatch(
            new Version1ProtocolMessageFactory()),
        new MessageDispatch(
            new Version2ProtocolMessageFactory())
    );
    system.start();
  }
}
