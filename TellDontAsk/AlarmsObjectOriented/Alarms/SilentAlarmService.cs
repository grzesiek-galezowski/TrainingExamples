using System;
using AlarmsObjectOriented.Adapters;
using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented.Alarms
{
  public class SilentAlarm : Alarm
  {
    private readonly string _numberToCall;

    public SilentAlarm(string numberToCall)
    {
      _numberToCall = numberToCall;
    }

    public void Trigger()
    {
      TelephoneService.Call(_numberToCall);
    }

    public void Disable()
    {
      TelephoneService.Recall(_numberToCall);
    }

    public void Dump()
    {
      Console.WriteLine("{ Calls: " + _numberToCall + " }");
    }
  }
}