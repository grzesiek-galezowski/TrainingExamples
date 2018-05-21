package Main;

import Enums.AlarmTypes;
import Enums.TimeCriteria;
  // 1. Composability - this example & OO. Differences - Composing behaviors
  // 2. Where tell don't ask does not apply
  // 3. Payroll example - Tell Don't Ask, what is a better abstraction?
  // 4. Sessions example - pass behavior!!! Centralized vs delegated control styles
  // 5. Interfaces vs classes vs events vs delegates (ClassesAreBadForComposability.cs)
  // 6. Small interfaces - segregation
  // 7. Protocols (ProtocolsExist.cs and so on)
  // 8. Mock Objects - outside in protocol design
  //10. Static fields and composability

public class CompositionRoot {
  private final static String SecurityPhoneNumber = "11-222-1121";

  public static void main() {
    Building building = new Building(createAlarm());

    building.someoneEntered();
    building.allClear();

    System.out.println("========DUMP=========");
    building.dump();

  }

  ///////////////////
  // COMP
  // -> TIMED -> LOUD
  // -> SILENT
  ///////////////////

  private static Alarm createAlarm() {
    Alarm alarm = new Alarm(AlarmTypes.Composite);

    alarm.nested1 = new Alarm(AlarmTypes.Timed);

    alarm.nested1.nested1 = new Alarm(AlarmTypes.Loud);
    alarm.nested1.timeCriterias = allOf(TimeCriteria.AtNight, TimeCriteria.OnWeekend);

    alarm.nested2 = new Alarm(AlarmTypes.Silent);
    alarm.nested2.numberToCall = SecurityPhoneNumber;

    return alarm;
  }

  public static TimeCriteria[] allOf(TimeCriteria... criteria) {
    return criteria;
  }
}


