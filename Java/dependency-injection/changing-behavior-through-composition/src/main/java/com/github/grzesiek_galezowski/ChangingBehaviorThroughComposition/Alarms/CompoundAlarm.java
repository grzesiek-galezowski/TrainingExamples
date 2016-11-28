package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarms;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarm;

public class CompoundAlarm implements Alarm {
  private final Alarm[] _alarms;

  public CompoundAlarm(Alarm... alarms) {
    _alarms = alarms;
  }

  public void trigger() {
    for (Alarm alarm : _alarms) {
      alarm.trigger();
    }
  }

  public void disable() {
    for (Alarm alarm : _alarms) {
      alarm.disable();
    }
  }
}
