package _01_Basic;

//Refactor this
//ctrl+alt+...

//TODO 1. rename begin
//TODO 2. move
//TODO 3. copy type
//TODO 4. safe delete
public class BasicRefactorings {
  public void Start() {
    begin();
    begin(1);
    middle(2, 3);
    end(3);
  }

  private void begin(int i) {
    System.out.println(i);
  }

  private void begin() {
    begin(1);
  }

  private void middle(int i, int notNeeded) {
    System.out.println(i);
  }

  private void end(int i) {
    System.out.println(i);
  }
}

class DontWantToBeHere //TODO move it to another namespace and another file
{
  public void lol() {
    fitsSomewhereElse();
  }

  private static void fitsSomewhereElse() //TODO move to another type and make instance method
  {

  }
}


