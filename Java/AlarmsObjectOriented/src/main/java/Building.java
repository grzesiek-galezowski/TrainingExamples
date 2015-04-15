import interfaces.Alarm;

public class Building {
  private final Alarm _alarm;

  public Building(Alarm alarm) {
    _alarm = alarm;
  }

  public void someoneEntered() {
    _alarm.trigger();
  }

  public void allClear() {
    _alarm.disable();
  }

  public void dump() {
    _alarm.dump();
  }
}

