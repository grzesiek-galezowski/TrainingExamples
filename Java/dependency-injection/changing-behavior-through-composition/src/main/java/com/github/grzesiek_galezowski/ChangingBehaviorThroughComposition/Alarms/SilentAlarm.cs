using System;

namespace ChangingBehaviorThroughComposition
{
  public class SilentAlarm : Alarm
  {
    private readonly string _phoneNumber;

    public SilentAlarm(string phoneNumber)
    {
      _phoneNumber = phoneNumber;
    }

    public void Trigger()
    {
      Console.WriteLine("Calling " + _phoneNumber);
    }

    public void Disable()
    {
      Console.WriteLine("Not calling " + _phoneNumber);
    }
  }
}