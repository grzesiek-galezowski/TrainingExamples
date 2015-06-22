package AlarmServices;

import Main.Alarm;

public class AlarmService {
    public static void triggerAlarm(Alarm alarm) {
      switch (alarm.alarmTypes) {
        case Loud:
          LoudAlarmService.trigger();
          break;
        case Silent:
          SilentAlarmService.trigger(alarm);
          break;
        case Composite:
          CompositeAlarmService.trigger(alarm);
          break;
        case Timed:
          TimedAlarmService.trigger(alarm);
          break;
        default:
          throw new IllegalArgumentException("out or range");
      }
    }

    public static void disableAlarm(Alarm alarm) {
      switch (alarm.alarmTypes) {
        case Loud:
          LoudAlarmService.disable();
          break;
        case Silent:
          SilentAlarmService.disable(alarm);
          break;
        case Composite:
          CompositeAlarmService.disable(alarm);
          break;
        case Timed:
          TimedAlarmService.disable(alarm);
          break;
        default:
          throw new IllegalArgumentException("out or range");
      }
    }

    public static void dump(Alarm alarm) {
      switch (alarm.alarmTypes) {
        case Loud:
          LoudAlarmService.dump(alarm);
          break;
        case Silent:
          SilentAlarmService.dump(alarm);
          break;
        case Composite:
          CompositeAlarmService.dump(alarm);
          break;
        case Timed:
          TimedAlarmService.dump(alarm);
          break;
        default:
          throw new IllegalArgumentException("out or range");
      }
    }
  }
