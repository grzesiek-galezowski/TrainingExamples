package _03_LessUsed;
//TODO replace constructor with factory method
//TODO copy type MyMessage with factory method / move static method
//TODO replace static method with instance one (not automated)
//TODO change returned type and rename to MyMessageFactory
//TODO use MyMessageFactory
//TODO inline method
//TODO for factory - introduce field (initialized from constructor)
//TODO for factory constructor invocation - introduce parameter
//TODO for factory - extract interface
//TODO for factory - use base type where possible

public class Object3 {
  public void doSomething() {
    final var message = new MyMessage(5, 6);
    message.send();
  }
}

