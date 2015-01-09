using DependencyInjectionBefore._1_ControlFreak.Core;

namespace DependencyInjectionBefore._1_ControlFreak
{
    public class ApplicationRoot
    {
      public void Main()
      {
        var sys = new TeleComSystem();

        sys.Start();
      }

      public void Main_IoC_Container()
      {
        //TODO mention RRR pattern

        var sys = new TeleComSystem();

        sys.Start();
      }

    }

}
