using DependencyInjection._1_ControlFreak.Core;

namespace DependencyInjection._1_ControlFreak
{
    public class ApplicationRoot
    {
      public void Main()
      {
        var sys = new TeleComSystem();

        sys.Start();
      }
    }

}
