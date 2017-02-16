using System;
using AlarmsProcedural.Enums;

namespace AlarmsProcedural.AlarmServices
{
  public static class AlarmService
  {
    public static void TriggerAlarm(Alarm alarm)
    {
      switch (alarm.AlarmType)
      {
        case AlarmTypes.Loud:
          LoudAlarmService.Trigger();
          break;
        case AlarmTypes.Silent:
          SilentAlarmService.Trigger(alarm);
          break;
        case AlarmTypes.Composite:
          CompositeAlarmService.Trigger(alarm);
          break;
        case AlarmTypes.Timed:
          TimedAlarmService.Trigger(alarm);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static void DisableAlarm(Alarm alarm)
    {
      switch (alarm.AlarmType)
      {
        case AlarmTypes.Loud:
          LoudAlarmService.Disable();
          break;
        case AlarmTypes.Silent:
          SilentAlarmService.Disable(alarm);
          break;
        case AlarmTypes.Composite:
          CompositeAlarmService.Disable(alarm);
          break;
        case AlarmTypes.Timed:
          TimedAlarmService.Disable(alarm);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    public static void Dump(Alarm alarm)
    {
      switch (alarm.AlarmType)
      {
        case AlarmTypes.Loud:
          LoudAlarmService.Dump();
          break;
        case AlarmTypes.Silent:
          SilentAlarmService.Dump(alarm);
          break;
        case AlarmTypes.Composite:
          CompositeAlarmService.Dump(alarm);
          break;
        case AlarmTypes.Timed:
          TimedAlarmService.Dump(alarm);
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}