package criterias;

import interfaces.TimeCriteria;

public class OutsideWeekendCriteria implements TimeCriteria {
    public boolean isSatisfied() {
      return !new WeekendCriteria().isSatisfied();
    }

    public void output() {
      System.out.println("it's not weekend");
    }
  }
