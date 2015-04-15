package AlarmServices;

import Adapters.TimeService;
import Enums.TimeCriteria;
import Main.Alarm;

import java.util.Arrays;


public class TimedAlarmService {
    public static void trigger(Alarm alarm) {
      if (Arrays.stream(alarm.timeCriterias).allMatch(c -> isSatisfied(c))) {
        AlarmService.triggerAlarm(alarm.nested1);
      }
    }

    private static boolean isSatisfied(TimeCriteria criteria) {
      switch (criteria) {
        case OnWeekend:
          return TimeService.isWeekend();
        case OutsideWeekend:
          return TimeService.isOutsideWeekend();
        case AtNight:
          return TimeService.isNight();
        case DuringDay:
          return TimeService.isDay();
        default:
          throw new IllegalArgumentException("criteria");
      }
    }

    public static void disable(Alarm alarm) {
      AlarmService.disableAlarm(alarm.nested1);
    }

    public static void dump(Alarm alarm) {
      System.out.println("{ Timed Alarm active when: ");
      outputCriterias(alarm);
      System.out.println("When triggered : ");
      AlarmService.dump(alarm.nested1);
      System.out.println(" }");
    }

    private static void outputCriterias(Alarm alarm) {
      for(TimeCriteria criteria : alarm.timeCriterias)
      {
        switch (criteria) {
          case OnWeekend:
            System.out.println("it's weekend");
            break;
          case OutsideWeekend:
            System.out.println("it's not weekend");
            break;
          case AtNight:
            System.out.println("it's night");
            break;
          case DuringDay:
            System.out.println("it's a day");
            break;
          default:
            throw new IllegalArgumentException("out or range");
        }
      }
    }
  }
