package Main;

import Enums.AlarmTypes;
import Enums.TimeCriteria;

public class Alarm {
  public Alarm(AlarmTypes alarmTypes) {
    this.alarmTypes = alarmTypes;
  }

  public AlarmTypes alarmTypes;

  public Alarm nested1;
  public Alarm nested2;
  public TimeCriteria[] timeCriterias;
  public String numberToCall;
}
