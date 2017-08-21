package _03_LessUsed;

public class ExampleSystem implements MySystem {
  private final Object1 object1;
  private final Object2 object2;
  private final Object3 object3;

  public ExampleSystem(Object1 object1, Object2 object2, Object3 object3) {
    this.object1 = object1;
    this.object2 = object2;
    this.object3 = object3;
  }

  public void lol() {
    object1.doSomething();
    object2.doSomething();
    object3.doSomething();
  }
}
