using AlarmsProcedural.AlarmServices;

namespace AlarmsProcedural
{
  public class Building
  {
    private readonly Alarm _alarm;

    public Building(Alarm alarm)
    {
      _alarm = alarm;
    }

    public void SomeoneEntered()
    {
      AlarmService.TriggerAlarm(_alarm);
    }

    public void AllClear()
    {
      AlarmService.DisableAlarm(_alarm);
    }

    public void Dump()
    {
      AlarmService.Dump(_alarm);
    }
  }
}