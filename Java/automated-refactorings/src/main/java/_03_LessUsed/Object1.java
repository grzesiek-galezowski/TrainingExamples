package _03_LessUsed;

public class Object1 {
  public void doSomething() {
    final var message = new MyMessage(1, 2);
    message.send();
  }
}
