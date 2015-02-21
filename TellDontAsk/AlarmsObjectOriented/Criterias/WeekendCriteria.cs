using System;
using AlarmsObjectOriented.Interfaces;
using TddEbook.TddToolkit;

namespace AlarmsObjectOriented.Criterias
{
  internal class WeekendCriteria : TimeCriteria
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