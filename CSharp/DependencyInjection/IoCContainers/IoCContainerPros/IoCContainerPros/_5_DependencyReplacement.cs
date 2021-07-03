using System.Reflection;
using Autofac;
using Autofac.Core.Registration;
using NSubstitute;
using NUnit.Framework;

namespace IoCContainerPros
{
  public class DependencyReplacement
  {
    [Test] 
    public void ShouldBeAbleToOverrideArbitraryDependencyInContainer()
    {
      var builder = new ContainerBuilder();

      builder.RegisterType<SomeLogic>().As<ISomeLogic>().SingleInstance();
      builder.RegisterType<TroublesomeDependency>().As<ITroublesomeDependency>().SingleInstance();

      var troublesomeDependencyMock = Substitute.For<ITroublesomeDependency>();
      builder.Register(_ => troublesomeDependencyMock);

      using (var container = builder.Build())
      {
        container.Resolve<ISomeLogic>().Execute();

        troublesomeDependencyMock.Received(1).DoSomething();
      }
    }

    [Test] 
    public void ShouldBeAbleToOverrideArbitraryDependency()
    {
      var logicRootForTests = new LogicRootForTests();
      logicRootForTests.GetSomeLogic().Execute();

      logicRootForTests.TroublesomeDependency.Received(1).DoSomething();
    }
  }

  public class TroublesomeDependency : ITroublesomeDependency
  {
    public void DoSomething()
    {
      throw new System.NotImplementedException();
    }
  }

  public interface ITroublesomeDependency
  {
    void DoSomething();
  }

  public class SomeLogic : ISomeLogic
  {
    private readonly ITroublesomeDependency _dep;

    public SomeLogic(ITroublesomeDependency dep)
    {
      _dep = dep;
    }

    public void Execute()
    {
      _dep.DoSomething();
    }
  }

  public interface ISomeLogic
  {
    void Execute();
  }
}
