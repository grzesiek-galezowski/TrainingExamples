using System;
using System.Collections.Generic;
using System.Linq;
using AlarmsObjectOriented.Adapters;
using AlarmsObjectOriented.Alarms;
using AlarmsObjectOriented.Criterias;
using AlarmsObjectOriented.Interfaces;
using NUnit.Framework;

namespace AlarmsObjectOriented
{
  public class CompositionRoot
  {
    private const string SecurityPhoneNumber = "11-222-1121";

    public static void Main()
    {
      var building = new Building(CreateAlarm());

      building.SomeoneEntered();
      building.AllClear();

      Console.WriteLine("========DUMP=========");
      building.Dump();

    }


    private static CompositeAlarm CreateAlarm()
    {
      return new CompositeAlarm
      (
        new TimedAlarm
        (
          new LoudAlarm(),
          AllOf
          (
            new NightCriteria(), 
            new WeekendCriteria()
          )
        ),
        new SilentAlarm
        (
          SecurityPhoneNumber
        )
      );
    }

    private static IEnumerable<TimeCriteria> AllOf(params TimeCriteria[] criterias)
    {
      return criterias;
    }
  }

  public class Spec
  {
    [Test]
    public void ShouldXxxxxxxxxxxx()
    {
      CompositionRoot.Main();
      Assert.Fail("Unfinished");
    }
  }
}
