using Castle.DynamicProxy;
using Command.Commands;
using NUnit.Framework;

namespace Command.Factories
{
  //aspect-oriented programming example
  public class SynchronizingInterceptor : IInterceptor
  {
    private readonly object _syncRoot = new object();

    public void Intercept(IInvocation invocation)
    {
      invocation.Proceed();
      var result = invocation.ReturnValue;
      invocation.ReturnValue = new SynchronizedCommand((InboundCommand) result, _syncRoot);
    }
  }

  /*
  public class Lol
  {
    [Test]
    public void Lol123()
    {
      var defaultGroupCommandFactory = Program.Synchronized(new DefaultGroupCommandFactory());

      var addGroupCommand = defaultGroupCommandFactory.CreateAddGroupCommand("");

      addGroupCommand.Execute();
    }
  }*/
}

