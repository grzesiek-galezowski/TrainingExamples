using System;
using AlarmsObjectOriented.Adapters;
using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented.Alarms
{
  public class LoudAlarm : Alarm
  {
    public void Trigger()
    {
      SirensService.PlayLoudSound();
    }

    public void Disable()
    {
      SirensService.StopPlayingLoudSound();
    }

    public void Dump()
    {
      Console.WriteLine("{ Playing loud sound }");
    }
  }
}