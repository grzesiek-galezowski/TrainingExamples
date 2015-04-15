package criterias;

import interfaces.TimeCriteria;

public class NightCriteria implements TimeCriteria {
  public boolean isSatisfied() {
    return !new DayCriteria().isSatisfied();
  }

  public void output() {
    System.out.println("it's night");
  }
}
