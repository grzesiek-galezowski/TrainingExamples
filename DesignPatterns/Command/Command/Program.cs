using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Command.Commands;
using Command.Factories;
using Microsoft.Owin.Hosting;

namespace Command
{
  class Program
  {
    private static readonly ProxyGenerator Generator = new ProxyGenerator();

    static void Main(string[] args)
    {
      string baseAddress = "http://*:80/";

      using (WebApp.Start<Startup>(url: baseAddress))
      {
        var commandFactory = new DefaultGroupCommandFactory();
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
