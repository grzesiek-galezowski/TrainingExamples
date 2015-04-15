package alarms;

import interfaces.Alarm;
import interfaces.TimeCriteria;

import java.util.Collection;

public class TimedAlarm implements Alarm {
  private final Alarm _alarm;
  private final Collection<TimeCriteria> _timeCriteria;

  public TimedAlarm(Alarm alarm, Collection<TimeCriteria> timeCriteria) {
    _alarm = alarm;
    _timeCriteria = timeCriteria;
  }

  public void trigger() {
    if (_timeCriteria.stream().allMatch(TimeCriteria::isSatisfied)) {
      _alarm.trigger();
    }
  }

  public void disable() {
    _alarm.disable();
  }

  public void dump() {
    System.out.println("{ Timed Alarm active when: ");
    OutputCriterias();
    System.out.println("When triggered : ");
    _alarm.dump();
    System.out.println(" }");
  }

  public void OutputCriterias() {
    for (TimeCriteria criteria : _timeCriteria) {
      criteria.output();
    }
  }
}
