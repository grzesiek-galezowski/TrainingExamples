using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented
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
      _alarm.Trigger();
    }

    public void AllClear()
    {
      _alarm.Disable();
    }

    public void Dump()
    {
      _alarm.Dump();
    }
  }

}