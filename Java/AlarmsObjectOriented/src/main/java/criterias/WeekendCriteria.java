package criterias;

import interfaces.TimeCriteria;

import static autofixture.publicinterface.Generate.any;

public class WeekendCriteria implements TimeCriteria {
  public boolean isSatisfied() {
    return any(boolean.class);
  }

  public void output() {
    System.out.println("it's weekend");
  }
}
