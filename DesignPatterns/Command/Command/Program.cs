using System.Threading;
using Castle.DynamicProxy;
using Command.DummyCode;
using Command.Factories;
using Microsoft.Owin.Hosting;

namespace Command
{
  //1. commands allow breaking from awkward inheritance
  //2. commands allow breaking away from big controllers gathering many dependencies
  //3. receiving results from commands - why passed through factory?
  //4. composability of commands
  //5. AOP example
  //6. Issues with commands

  internal static class Program
  {
    private static readonly ProxyGenerator Generator = new ProxyGenerator();

    static void Main(string[] args)
    {
      string baseAddress = "http://*:80/";

      using (WebApp.Start<Startup>(url: baseAddress))
      {
        var whatever = new DummyDep();
        var commandFactory = new DefaultGroupCommandFactory(whatever, whatever, whatever, whatever, whatever);
        new GroupsController(Synchronized(commandFactory), commandFactory);
        Thread.Sleep(Timeout.Infinite);
      }
    }

    public static GroupCommandFactory Synchronized(DefaultGroupCommandFactory defaultGroupCommandFactory)
    {
      return Generator.CreateInterfaceProxyWithTarget<GroupCommandFactory>(
        defaultGroupCommandFactory, new SynchronizingInterceptor());
    }
  }
}
