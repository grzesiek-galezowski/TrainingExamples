package com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarms;

import com.github.grzesiek_galezowski.ChangingBehaviorThroughComposition.Alarm;

public class SilentAlarm implements Alarm
  {
    private final String _phoneNumber;

    public SilentAlarm(String phoneNumber)
    {
      _phoneNumber = phoneNumber;
    }

    public void trigger()
    {
      System.out.println("Calling " + _phoneNumber);
    }

    public void disable()
    {
      System.out.println("Not calling " + _phoneNumber);
    }
  }
