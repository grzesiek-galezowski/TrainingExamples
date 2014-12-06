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

  //TODO 3rd Party Connect
  //TODO Composition Root
  //TODO 
  //TODO wrong use of DI container (
  //container as singleton, 
  //container used everywhere, 
  //container used only in constructors, 
  //container in composition root, 
  //container as a factory
  //TODO good use of DI container
  //TODO DI container - good and bad
  //TODO Composition Root DSL
  //TODO managing disposables with DI
  //TODO register-resolve-release
  //TODO DI - cycles - 2 solutions (1. Passing to one side in method arguments instead of constructor, 2. Events/setters)
}
