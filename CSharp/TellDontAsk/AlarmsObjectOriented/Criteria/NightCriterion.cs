using System;
using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented.Criteria
{
  internal class NightCriterion : TimeCriterion
  {
    public bool IsSatisfied()
    {
      return !new DayCriterion().IsSatisfied();
    }

    public void Output()
    {
      Console.WriteLine("it's night");
    }
  }
}