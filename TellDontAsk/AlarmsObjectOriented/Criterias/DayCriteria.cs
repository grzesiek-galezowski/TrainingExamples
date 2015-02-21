using System;
using AlarmsObjectOriented.Interfaces;
using TddEbook.TddToolkit;

namespace AlarmsObjectOriented.Criterias
{
  internal class DayCriteria : TimeCriteria
  {
    public bool IsSatisfied()
    {
      return Any.Boolean();
    }

    public void Output()
    {
      Console.WriteLine("it's a day");
    }
  }
}