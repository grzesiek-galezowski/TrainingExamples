public class Building {
  private final SilentAlarm alarm;

  public Building() {
    //what if we want to use another alarm?
    alarm = new SilentAlarm("333-444-555");
  }

  public void someoneCameIn() {
    alarm.trigger();
  }

  public void situationIsClear() {
    alarm.disable();
  }
}
