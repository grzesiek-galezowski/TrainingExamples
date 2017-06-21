using AlarmsProcedural.Enums;

namespace AlarmsProcedural
{
  public class Alarm
  {
    public Alarm(AlarmTypes alarmType)
    {
      AlarmType = alarmType;
    }

    public AlarmTypes AlarmType
    {
      get; set; 
    }

    public Alarm Nested1 { get; set; }
    public Alarm Nested2 { get; set; }
    public TimeCriteria[] TimeCriteria { get; set; }
    public string NumberToCall { get; set; }
  }
}