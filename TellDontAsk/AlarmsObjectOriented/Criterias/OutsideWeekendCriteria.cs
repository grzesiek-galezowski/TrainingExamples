using System;
using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented.Criterias
{
  public class OutsideWeekendCriteria : TimeCriteria
  {
    public bool IsSatisfied()
    {
      return !new WeekendCriteria().IsSatisfied();
    }

    public void Output()
    {
      Console.WriteLine("it's not weekend");
    }
  }
}