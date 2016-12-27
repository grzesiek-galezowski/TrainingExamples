using System;

namespace AlarmsWithNestedFunctions.Alarms
{
  public class LoudAlarm : Alarm
  {
    public void Trigger()
    {
      Console.WriteLine("Playing very loud sound!");
    }

    public void Disable()
    {
      Console.WriteLine("Not playing very loud sound anymore");
    }
  }
}