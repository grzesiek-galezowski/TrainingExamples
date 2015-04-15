package AlarmServices;

import Adapters.SirensService;
import Main.Alarm;

public class LoudAlarmService {
    public static void trigger() {
      SirensService.playLoudSound();
    }

    public static void disable() {
      SirensService.stopPlayingLoudSound();
    }

    public static void dump(Alarm alarm) {
      System.out.println("{ Playing loud sound }");
    }
  }
