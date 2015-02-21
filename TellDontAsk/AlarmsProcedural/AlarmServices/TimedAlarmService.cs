using System;
using System.Linq;
using AlarmsProcedural.Adapters;
using AlarmsProcedural.Enums;

namespace AlarmsProcedural.AlarmServices
{
  public static class TimedAlarmService
  {
    public static void Trigger(Alarm alarm)
    {
      if (alarm.TimeCriterias.All(IsSatisfied))
      {
        AlarmService.TriggerAlarm(alarm.Nested1);
      }
    }

    private static bool IsSatisfied(TimeCriterias criteria)
    {
      switch (criteria)
      {
        case TimeCriterias.OnWeekend:
          return TimeService.IsWeekend();
        case TimeCriterias.OutsideWeekend:
          return TimeService.IsOutsideWeekend();
        case TimeCriterias.AtNight:
          return TimeService.IsNight();
        case TimeCriterias.DuringDay:
          return TimeService.IsDay();
        default:
          throw new ArgumentOutOfRangeException("criteria");
      }
    }

    public static void Disable(Alarm alarm)
    {
      AlarmService.DisableAlarm(alarm.Nested1);
    }

    public static void Dump(Alarm alarm)
    {
      Console.WriteLine("{ Timed Alarm active when: ");
      OutputCriterias(alarm);
      Console.WriteLine("When triggered : ");
      AlarmService.Dump(alarm.Nested1);
      Console.WriteLine(" }");
    }

    private static void OutputCriterias(Alarm alarm)
    {
      foreach (var criteria in alarm.TimeCriterias)
      {
        switch (criteria)
        {
          case TimeCriterias.OnWeekend:
            Console.WriteLine("it's weekend");
            break;
          case TimeCriterias.OutsideWeekend:
            Console.WriteLine("it's not weekend");
            break;
          case TimeCriterias.AtNight:
            Console.WriteLine("it's night");
            break;
          case TimeCriterias.DuringDay:
            Console.WriteLine("it's a day");
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }
  }
}