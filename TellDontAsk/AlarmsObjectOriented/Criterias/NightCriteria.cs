using System;
using AlarmsObjectOriented.Interfaces;

namespace AlarmsObjectOriented.Criterias
{
  internal class NightCriteria : TimeCriteria
  {
    public bool IsSatisfied()
    {
      return !new DayCriteria().IsSatisfied();
    }

    public void Output()
    {
      Console.WriteLine("it's night");
    }
  }
}