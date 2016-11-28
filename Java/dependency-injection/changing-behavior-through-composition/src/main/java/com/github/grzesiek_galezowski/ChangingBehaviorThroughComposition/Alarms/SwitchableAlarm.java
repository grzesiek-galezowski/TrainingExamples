package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarms;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarm;
import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriteria;

public class SwitchableAlarm implements Alarm {
  private final Alarm _defaultAlarm;
  private final Alarm _secondaryAlarm;
  private final SwitchCriteria _switchCriteria;

  public SwitchableAlarm(SwitchCriteria switchCriteria, Alarm defaultAlarm, Alarm secondaryAlarm) {
    _defaultAlarm = defaultAlarm;
    _secondaryAlarm = secondaryAlarm;
    _switchCriteria = switchCriteria;
  }

  public void trigger() {
    if (_switchCriteria.isNotMet()) {
      _defaultAlarm.trigger();
    } else {
      _secondaryAlarm.trigger();
    }
  }

  public void disable() {
    _defaultAlarm.disable();
    _secondaryAlarm.disable();
  }
}
