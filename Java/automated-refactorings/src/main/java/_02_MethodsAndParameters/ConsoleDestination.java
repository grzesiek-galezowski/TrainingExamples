package _02_MethodsAndParameters;

class ConsoleDestination implements MessageDestination {
  public void send(String s) {
    System.out.println(s);
  }
}