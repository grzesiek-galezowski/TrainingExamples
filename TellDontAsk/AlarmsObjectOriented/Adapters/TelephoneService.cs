using System;

namespace AlarmsObjectOriented.Adapters
{
  public static class TelephoneService
  {
    public static void Call(string numberToCall)
    {
      Console.WriteLine("Calling " + numberToCall);
    }

    public static void Recall(string numberToCall)
    {
      Console.WriteLine("Recalling " + numberToCall);
    }
  }
}