namespace AlarmsWithNestedFunctions
{
    public class Building
    {
      private readonly Alarm _alarm;

      public Building(Alarm alarm)
      {
        _alarm = alarm;
      }

      public void SomeoneCameIn()
      {
        _alarm.Trigger();
      }

      public void SituationIsClear()
      {
        _alarm.Disable();
      }
    }
}
