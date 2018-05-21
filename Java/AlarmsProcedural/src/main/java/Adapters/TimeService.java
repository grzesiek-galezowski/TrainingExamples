package Adapters;

import autofixture.publicinterface.Any;

public class TimeService {
  public static boolean isWeekend() {
    return Any.instanceOf(boolean.class);
  }

  public static boolean isOutsideWeekend() {
    return !isWeekend();
  }

  public static boolean isNight() {
    return Any.instanceOf(boolean.class);
  }

  public static boolean isDay() {
    return !isNight();
  }
}
