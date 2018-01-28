public class Building {
  private final LoudAlarm alarm;

  public Building() {
    alarm = new LoudAlarm();
  }

  public void someoneCameIn() {
    alarm.trigger();
  }

  public void situationIsClear() {
    alarm.disable();
  }
}
