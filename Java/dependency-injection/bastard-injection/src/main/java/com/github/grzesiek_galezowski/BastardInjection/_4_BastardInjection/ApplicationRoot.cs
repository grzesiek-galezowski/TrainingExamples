using BastardInjection._4_BastardInjection.Core;

namespace BastardInjection._4_BastardInjection
{
    public class ApplicationRoot
    {
      public static void Main(string[] args)
      {
        var sys = new TeleComSystem(); //bastard injected - look at output socket which is disposable in this example!
        sys.Start();

        sys.Dispose(); //!!!!!!!!!!!!!!!!!!!!!!!!!!!! error!!!! 
      }
    }




}
