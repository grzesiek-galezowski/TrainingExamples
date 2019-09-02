using System;
using Autofac;
using NUnit.Framework;

namespace IoCContainerPros
{
  public class LifetimeManagement
  {
    [Test]
    public void ShouldDisposeOfCreatedDependencies()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterType<Lol>();
      using (var container = containerBuilder.Build())
      {
        var lol1 = container.Resolve<Lol>();
        var lol2 = container.Resolve<Lol>();
        Console.WriteLine("opening scope");
        using (var nested = container.BeginLifetimeScope("nested"))
        {
          var lol3 = nested.Resolve<Lol>();
          var lol4 = nested.Resolve<Lol>();
          Console.WriteLine("closing scope");
        }
        Console.WriteLine("closed scope");
        var lol5 = container.Resolve<Lol>();
      }
    }
  }

  public class Lol : IDisposable
  {
    private static int _counter = 0;
    private readonly int _currentId;

    public Lol()
    {
      _currentId = _counter++;
      Console.WriteLine("_____CREATED______" + _currentId);
    }

    public void Dispose()
    {
      Console.WriteLine("_____DISPOSED______" + _currentId);
    }
  }
}