using System;

namespace AlarmsProcedural.AlarmServices
{
  public static class CompositeAlarmService
  {
    public static void Trigger(Alarm alarm)
    {
      AlarmService.TriggerAlarm(alarm.Nested1);
      AlarmService.TriggerAlarm(alarm.Nested2);
    }

    public static void Disable(Alarm alarm)
    {
      AlarmService.DisableAlarm(alarm.Nested1);
      AlarmService.DisableAlarm(alarm.Nested2);
    }

    public static void Dump(Alarm alarm)
    {
      Console.WriteLine("{ Both: ");
      AlarmService.Dump(alarm.Nested1);
      AlarmService.Dump(alarm.Nested2);
      Console.WriteLine("} ");
    }
  }
}