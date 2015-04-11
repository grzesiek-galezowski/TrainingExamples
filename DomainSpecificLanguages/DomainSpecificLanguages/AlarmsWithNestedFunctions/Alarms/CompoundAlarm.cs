namespace ChangingBehaviorThroughComposition
{
  public class CompoundAlarm : Alarm
  {
    private readonly Alarm[] _alarms;
  
    public CompoundAlarm(params Alarm[] alarms)
    {
      _alarms = alarms;
    }

    public void Trigger()
    {
      foreach (var alarm in _alarms)
      {
        alarm.Trigger();
      }
    }

    public void Disable()
    {
      foreach (var alarm in _alarms)
      {
        alarm.Disable();
      }
    }
  }
}