package alarms;

import interfaces.Alarm;
import adapters.SirensService;

public class LoudAlarm implements Alarm {
  public void trigger() {
    SirensService.playLoudSound();
  }

  public void disable() {
    SirensService.stopPlayingLoudSound();
  }

  public void dump() {
    System.out.println("{ Playing loud sound }");
  }
}
