package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriterias;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriteria;

import java.time.DayOfWeek;
import java.time.LocalDate;

public class MetOnWorkDayCriteria implements SwitchCriteria {
  public boolean isNotMet() {
    return isWeekend();
  }

  private boolean isWeekend() {
    DayOfWeek dayOfWeek = LocalDate.now().getDayOfWeek();
    return dayOfWeek.equals(DayOfWeek.SATURDAY) || dayOfWeek.equals(DayOfWeek.SUNDAY);
  }
}
