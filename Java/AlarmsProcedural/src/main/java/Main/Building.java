package Main;

import AlarmServices.AlarmService;

public class Building {
  private final Alarm _alarm;

  public Building(Alarm alarm) {
    _alarm = alarm;
  }

  public void someoneEntered() {
    AlarmService.triggerAlarm(_alarm);
  }

  public void allClear() {
    AlarmService.disableAlarm(_alarm);
  }

  public void dump() {
    AlarmService.dump(_alarm);
  }
}
