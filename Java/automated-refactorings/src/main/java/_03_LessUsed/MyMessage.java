package _03_LessUsed;

public class MyMessage {
  private final int innerValue1;
  private final int innerValue2;

  public MyMessage(int i, int i1) {
    innerValue1 = i;
    innerValue2 = i1;
  }

  public int getValue1() {
    return innerValue1;
  }

  public int getValue2() {
    return innerValue2;
  }

  public void send() {
    throw new RuntimeException();
  }
}
