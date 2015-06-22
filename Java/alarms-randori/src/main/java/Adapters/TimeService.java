package Adapters;

import static autofixture.publicinterface.Generate.any;

public class TimeService {
  public static boolean isWeekend() {
    return any(boolean.class);
  }

  public static boolean isOutsideWeekend() {
    return !isWeekend();
  }

  public static boolean isNight() {
    return any(boolean.class);
  }

  public static boolean isDay() {
    return !isNight();
  }
}
