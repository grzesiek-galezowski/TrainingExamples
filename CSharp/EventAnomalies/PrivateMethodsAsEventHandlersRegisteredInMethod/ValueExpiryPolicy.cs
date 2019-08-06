using System;

namespace PrivateMethodsAsEventHandlersRegisteredInMethod
{
  public class ValueExpiryPolicy
  {
    public event EventHandler<RemoveEventArgs> ItemNotNeededAnymore;
    //...

    public void Lol()
    {
      ItemNotNeededAnymore?.Invoke(this, new RemoveEventArgs()
      {
        Key = "AAA"
      });
    }
  }
}