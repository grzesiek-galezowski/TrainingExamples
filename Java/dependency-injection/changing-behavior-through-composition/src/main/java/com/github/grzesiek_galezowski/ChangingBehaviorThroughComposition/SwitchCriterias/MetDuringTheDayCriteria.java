package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriterias;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.SwitchCriteria;

import java.time.LocalTime;

public class MetDuringTheDayCriteria implements SwitchCriteria
  {
    public boolean isNotMet()
    {
      //buggy implementation. Nevermind...
      return isNight();
    }

    private static boolean isNight()
    {
      int currentHour = LocalTime.now().getHour();
      return currentHour > 22 && currentHour < 8;
    }
  }
