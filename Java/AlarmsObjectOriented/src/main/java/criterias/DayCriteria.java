package criterias;

import interfaces.TimeCriteria;

import static autofixture.publicinterface.Generate.any;

public class DayCriteria implements TimeCriteria {
  public boolean isSatisfied() {
    return any(Boolean.class);
  }

  public void output() {
    System.out.println("it's a day");
  }
}
