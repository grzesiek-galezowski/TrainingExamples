package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarms;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarm;

public class LoudAlarm implements Alarm {
  public void trigger() {
    System.out.println("Playing very loud sound!");
  }

  public void disable() {
    System.out.println("Not playing very loud sound anymore");
  }
}
