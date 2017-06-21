using System;
using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented.Alarms
{
  public class CompositeAlarm : Alarm
  {
    private readonly Alarm _alarmService1;
    private readonly Alarm _alarmService2;

    public CompositeAlarm(Alarm alarmService1, Alarm alarmService2)
    {
      _alarmService1 = alarmService1;
      _alarmService2 = alarmService2;
    }

    public void Trigger()
    {
      _alarmService1.Trigger();
      _alarmService2.Trigger();
    }

    public void Disable()
    {
      _alarmService1.Disable();
      _alarmService2.Disable();
    }

    public void Dump()
    {
      Console.WriteLine("{ Both: ");
      _alarmService1.Dump();
      _alarmService2.Dump();
      Console.WriteLine("} ");
    }
  }
}