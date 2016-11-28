namespace ChangingBehaviorThroughComposition
{
  public class SwitchableAlarm : Alarm
  {
    private readonly Alarm _defaultAlarm;
    private readonly Alarm _secondaryAlarm;
    private readonly SwitchCriteria _switchCriteria;

    public SwitchableAlarm(SwitchCriteria switchCriteria, Alarm defaultAlarm, Alarm secondaryAlarm)
    {
      _defaultAlarm = defaultAlarm;
      _secondaryAlarm = secondaryAlarm;
      _switchCriteria = switchCriteria;
    }
  
    public void Trigger()
    {
      if(_switchCriteria.IsNotMet())
      {
        _defaultAlarm.Trigger();
      }
      else
      {
        _secondaryAlarm.Trigger();
      }
    }

    public void Disable()
    {
      _defaultAlarm.Disable();
      _secondaryAlarm.Disable();
    }
  }
}