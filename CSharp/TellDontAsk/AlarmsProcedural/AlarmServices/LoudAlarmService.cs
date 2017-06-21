using System;
using AlarmsProcedural.Adapters;

namespace AlarmsProcedural.AlarmServices
{
  public static class LoudAlarmService
  {
    public static void Trigger()
    {
      SirensService.PlayLoudSound();
    }

    public static void Disable()
    {
      SirensService.StopPlayingLoudSound();
    }

    public static void Dump()
    {
      Console.WriteLine("{ Playing loud sound }");
    }
  }
}