using System;
using AlarmsProcedural.Enums;
using NUnit.Framework;

namespace AlarmsProcedural
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

    private static Alarm CreateAlarm()
    {
      return new Alarm(AlarmTypes.Composite)
      {
        Nested1 = new Alarm(AlarmTypes.Timed)
        {
          Nested1 = new Alarm(AlarmTypes.Loud),
          TimeCriterias = AllOf
          (
            TimeCriterias.AtNight, 
            TimeCriterias.OnWeekend
          ),
        },
        Nested2 = new Alarm(AlarmTypes.Silent)
        {
          NumberToCall = SecurityPhoneNumber
        }
      };
    }

    public static TimeCriterias[] AllOf(params TimeCriterias[] criterias)
    {
      return criterias;
    }
  }

  public class Spec
  {
    [Test]
    public void ShouldXXXXXXXXXXXX()
    {
      CompositionRoot.Main();
      Assert.Fail("Unfinished");
    }
  }
}
