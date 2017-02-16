using System;
using AlarmsObjectOriented.Interfaces;
using TddEbook.TddToolkit;

namespace AlarmsObjectOriented.Criteria
{
  internal class WeekendCriterion : TimeCriterion
  {
    public bool IsSatisfied()
    {
      return Any.Boolean();
    }

    public void Output()
    {
      Console.WriteLine("it's weekend");
    }
  }
}