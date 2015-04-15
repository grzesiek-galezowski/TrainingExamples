package alarms;

import interfaces.Alarm;

public class CompositeAlarm implements Alarm {
  private final Alarm _alarmService1;
  private final Alarm _alarmService2;

  public CompositeAlarm(Alarm alarmService1, Alarm alarmService2) {
    _alarmService1 = alarmService1;
    _alarmService2 = alarmService2;
  }

  public void trigger() {
    _alarmService1.trigger();
    _alarmService2.trigger();
  }

  public void disable() {
    _alarmService1.disable();
    _alarmService2.disable();
  }

  public void dump() {
    System.out.println("{ Both: ");
    _alarmService1.dump();
    _alarmService2.dump();
    System.out.println("} ");
  }
}
