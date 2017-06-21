using System;

namespace AlarmsWithNestedFunctions.SwitchCriterias
{
  public class MetDuringTheDayCriteria : SwitchCriteria
  {
    public bool IsNotMet()
    {
      //buggy implementation. Nevermind...
      return IsNight();
    }

    private static bool IsNight()
    {
      var currentHour = DateTime.Now.Hour;
      return currentHour > 22 && currentHour < 8;
    }
  }
}