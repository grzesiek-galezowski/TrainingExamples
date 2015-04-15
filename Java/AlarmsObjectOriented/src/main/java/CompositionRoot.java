import alarms.CompositeAlarm;
import alarms.LoudAlarm;
import alarms.SilentAlarm;
import alarms.TimedAlarm;
import criterias.NightCriteria;
import criterias.WeekendCriteria;
import interfaces.TimeCriteria;

import java.util.Arrays;
import java.util.Collection;

public class CompositionRoot {
  private final static String SecurityPhoneNumber = "11-222-1121";

  public static void main() {
    Building building = new Building(createAlarm());

    building.someoneEntered();
    building.allClear();

    System.out.println("========DUMP=========");
    building.dump();

  }


  private static CompositeAlarm createAlarm() {
    return new CompositeAlarm (
        new TimedAlarm (
            new LoudAlarm(),
            allOf (
                new NightCriteria(),
                new WeekendCriteria()
              )
          ),
        new SilentAlarm (
            SecurityPhoneNumber
          )
      );
  }

  private static Collection<TimeCriteria> allOf(TimeCriteria... criterias) {
    return Arrays.asList(criterias);
  }
}
