package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarms;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarm;

import java.util.Arrays;

public class ParallelCompoundAlarm implements Alarm {
  private final Alarm[] _alarms;

  public ParallelCompoundAlarm(Alarm... alarms) {
    _alarms = alarms;
  }

  public void trigger() {
    Arrays.stream(_alarms).parallel().forEach(alarm -> alarm.trigger());
  }

  public void disable() {
    Arrays.stream(_alarms).parallel().forEach(alarm -> alarm.trigger());
  }
}
