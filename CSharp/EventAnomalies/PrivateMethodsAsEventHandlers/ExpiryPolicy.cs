using System;

namespace PrivateMethodsAsEventHandlers
{
  public class ExpiryPolicy
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