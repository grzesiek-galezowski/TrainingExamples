package _03_LessUsed;

public class CompositionRoot {
  public MySystem resolve() {
    return new ExampleSystem(new Object1(), new Object2(), new Object3());
  }
}

