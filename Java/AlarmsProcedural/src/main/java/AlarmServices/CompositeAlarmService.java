package AlarmServices;

import Main.Alarm;

public class CompositeAlarmService {
  public static void trigger(Alarm alarm) {
    AlarmService.triggerAlarm(alarm.nested1);
    AlarmService.triggerAlarm(alarm.nested2);
  }

  public static void disable(Alarm alarm) {
    AlarmService.disableAlarm(alarm.nested1);
    AlarmService.disableAlarm(alarm.nested2);
  }

  public static void dump(Alarm alarm) {
    System.out.println("{ Both: ");
    AlarmService.dump(alarm.nested1);
    AlarmService.dump(alarm.nested2);
    System.out.println("} ");
  }
}
