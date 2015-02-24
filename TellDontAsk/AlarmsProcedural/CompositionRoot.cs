using System;
using AlarmsProcedural.Enums;
using NUnit.Framework;

namespace AlarmsProcedural
{
  // 1. Composability - this example & OO. Differences - Composing behaviors
  // 2. Where tell don't ask does not apply
  // 3. Payroll example - Tell Don't Ask, what is a better abstraction?
  // 4. Sessions example - pass behavior!!! Centralized vs delegated control styles
  // 5. Interfaces vs classes vs events vs delegates (ClassesAreBadForComposability.cs)
  // 6. Small interfaces - segregation
  // 7. Protocols (ProtocolsExist.cs and so on)
  // 8. Mock Objects - outside in protocol design
  //10. Static fields and composability

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
