using TddEbook.TddToolkit;

namespace AlarmsProcedural.Adapters
{
  static class TimeService
  {
    public static bool IsWeekend()
    {
      return Any.Boolean();
    }

    public static bool IsOutsideWeekend()
    {
      return !IsWeekend();
    }

    public static bool IsNight()
    {
      return Any.Boolean();
    }

    public static bool IsDay()
    {
      return !IsNight();
    }
  }
}