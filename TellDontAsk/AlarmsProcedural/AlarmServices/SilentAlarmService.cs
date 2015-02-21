using System;
using AlarmsProcedural.Adapters;

namespace AlarmsProcedural.AlarmServices
{
  public static class SilentAlarmService
  {
    public static void Trigger(Alarm alarm)
    {
      TelephoneService.Call(alarm.NumberToCall);
    }

    public static void Disable(Alarm alarm)
    {
      TelephoneService.Recall(alarm.NumberToCall);
    }

    public static void Dump(Alarm alarm)
    {
      Console.WriteLine("{ Calls: " + alarm.NumberToCall + " }");
    }
  }
}