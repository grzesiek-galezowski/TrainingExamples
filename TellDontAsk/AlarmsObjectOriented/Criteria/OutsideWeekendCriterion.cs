using System;
using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented.Criteria
{
  public class OutsideWeekendCriterion : TimeCriterion
  {
    public bool IsSatisfied()
    {
      return !new WeekendCriterion().IsSatisfied();
    }

    public void Output()
    {
      Console.WriteLine("it's not weekend");
    }
  }
}