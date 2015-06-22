package AlarmServices;

import Adapters.TelephoneService;
import Main.Alarm;

public class SilentAlarmService {
    public static void trigger(Alarm alarm) {
      TelephoneService.call(alarm.numberToCall);
    }

    public static void disable(Alarm alarm) {
      TelephoneService.recall(alarm.numberToCall);
    }

    public static void dump(Alarm alarm) {
      System.out.println("{ Calls: " + alarm.numberToCall + " }");
    }
  }
