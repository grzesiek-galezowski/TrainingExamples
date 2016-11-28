package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition;

public class Building {
  private final Alarm _alarm;

  public Building(Alarm alarm) {
    _alarm = alarm;
  }

  public void someoneCameIn() {
    _alarm.trigger();
  }

  public void situationIsClear() {
    _alarm.disable();
  }
}
