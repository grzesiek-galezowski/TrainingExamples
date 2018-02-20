package com.github.TrainingExamples.grzesiek_galezowski;

public class Main {
  public static void main() {
    MySystem system = new MySystem(
        //one endpoint
        new MessageDispatch(
            new V1ProtocolMessageFactory()),
        //another endpoint
        new MessageDispatch(
            new V2ProtocolMessageFactory())
    );
    system.start();
  }
}
