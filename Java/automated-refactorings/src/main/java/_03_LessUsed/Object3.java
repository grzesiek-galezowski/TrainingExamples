package _03_LessUsed;
//Way 1:
//TODO replace constructor with factory method
//TODO Move
//TODO replace static method with instance one (not automated)
//TODO change returned type and rename to MyMessageFactory
//TODO use MyMessageFactory
//TODO inline method
//TODO for factory - introduce field (initialized from constructor)
//TODO for factory constructor invocation - introduce parameter
//TODO for factory - extract interface
//TODO for factory - use base type where possible
//Way 2: extract method, introduce param object, convert to instance method etc.


public class Object3 {
  public void doSomething() {
    final var message = new MyMessage(5, 6);
    message.send();
  }
}

