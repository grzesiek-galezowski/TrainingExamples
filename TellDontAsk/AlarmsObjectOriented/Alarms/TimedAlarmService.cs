using System;
using System.Collections.Generic;
using System.Linq;
using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented.Alarms
{
  public class TimedAlarm : Alarm
  {
    private readonly Alarm _alarm;
    private readonly IEnumerable<TimeCriterion> _timeServices;

    public TimedAlarm(Alarm alarm, IEnumerable<TimeCriterion> timeServices)
    {
      _alarm = alarm;
      _timeServices = timeServices;
    }

    public void Trigger()
    {
      if (_timeServices.All(c => c.IsSatisfied()))
      {
        _alarm.Trigger();
      }
    }

    public void Disable()
    {
      _alarm.Disable();
    }

    public void Dump()
    {
      Console.WriteLine("{ Timed Alarm active when: ");
      OutputCriteria();
      Console.WriteLine("When triggered : ");
      _alarm.Dump();
      Console.WriteLine(" }");
    }

    public void OutputCriteria()
    {
      foreach (var criteria in _timeServices)
      {
         criteria.Output();
      }
    }
  }
}