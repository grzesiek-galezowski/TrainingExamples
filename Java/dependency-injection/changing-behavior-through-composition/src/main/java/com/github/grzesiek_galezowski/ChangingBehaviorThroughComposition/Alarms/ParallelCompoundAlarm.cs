using System.Threading.Tasks;

namespace ChangingBehaviorThroughComposition
{
  public class ParallelCompoundAlarm : Alarm
  {
    private readonly Alarm[] _alarms;

    public ParallelCompoundAlarm(params Alarm[] alarms)
    {
      _alarms = alarms;
    }

    public void Trigger()
    {
      Parallel.ForEach(_alarms, alarm => alarm.Trigger());
    }

    public void Disable()
    {
      Parallel.ForEach(_alarms, alarm => alarm.Trigger());
    }
  }
}