using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using NUnit.Framework;

namespace IoCContainerPros
{
  public class Interception
  {
    [Test]
    public void ShouldEnableInterception()
    {
      var containerBuilder = new ContainerBuilder();
      containerBuilder
        .RegisterType<Lol2>().As<ILol2>()
        .EnableInterfaceInterceptors()
        .InterceptedBy(typeof(CallLogger));
      containerBuilder.RegisterType<CallLogger>();
      
      using (var container = containerBuilder.Build())
      {
        var lol2 = container.Resolve<ILol2>();
        lol2.DoSomething();
      }
    }
  }

  public interface ILol2
  {
    void DoSomething();
  }

  public class Lol2 : ILol2
  {
    public void DoSomething()
    {
      
    }
  }

  public class CallLogger : IInterceptor
  {
    public void Intercept(IInvocation invocation)
    {
      Console.WriteLine("Called " + invocation.Method.Name);
    }
  }

}