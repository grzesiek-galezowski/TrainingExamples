using System;

namespace AlarmsWithNestedFunctions.SwitchCriterias
{
  public class MetOnWorkDayCriteria : SwitchCriteria
  {
    public bool IsNotMet()
    {
      return IsWeekend();
    }

    private bool IsWeekend()
    {
      var dayOfWeek = DateTime.Now.DayOfWeek;
      return dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
    }
  }
}