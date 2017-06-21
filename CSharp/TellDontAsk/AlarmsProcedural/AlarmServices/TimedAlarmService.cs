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
      if (CriteriaAreSatisfied(alarm))
      {
        AlarmService.TriggerAlarm(alarm.Nested1);
      }
    }

    private static bool IsSatisfied(TimeCriteria criterion)
    {
      switch (criterion)
      {
        case TimeCriteria.OnWeekend:
          return TimeService.IsWeekend();
        case TimeCriteria.OutsideWeekend:
          return TimeService.IsOutsideWeekend();
        case TimeCriteria.AtNight:
          return TimeService.IsNight();
        case TimeCriteria.DuringDay:
          return TimeService.IsDay();
        default:
          throw new ArgumentOutOfRangeException("criterion");
      }
    }

    public static void Disable(Alarm alarm)
    {
      AlarmService.DisableAlarm(alarm.Nested1);
    }

    public static void Dump(Alarm alarm)
    {
      Console.WriteLine("{ Timed Alarm active when: ");
      OutputCriteria(alarm);
      Console.WriteLine("When triggered : ");
      AlarmService.Dump(alarm.Nested1);
      Console.WriteLine(" }");
    }

    private static void OutputCriteria(Alarm alarm)
    {
      foreach (var criteria in alarm.TimeCriteria)
      {
        switch (criteria)
        {
          case TimeCriteria.OnWeekend:
            Console.WriteLine("it's weekend");
            break;
          case TimeCriteria.OutsideWeekend:
            Console.WriteLine("it's not weekend");
            break;
          case TimeCriteria.AtNight:
            Console.WriteLine("it's night");
            break;
          case TimeCriteria.DuringDay:
            Console.WriteLine("it's a day");
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }
    }

    private static bool CriteriaAreSatisfied(Alarm alarm)
    {
      return alarm.TimeCriteria.All(IsSatisfied);
    }

  }
}