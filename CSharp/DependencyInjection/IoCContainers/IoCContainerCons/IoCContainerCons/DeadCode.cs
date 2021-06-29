using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
  public class DeadCode
  {
    [Test]
    public void ContainerContainsSomeDeadCode()
    {
      //GIVEN
      var builder = new ContainerBuilder();
      builder.RegisterType<Dependency>().SingleInstance();
      builder.RegisterType<DependencyConsumer>().SingleInstance();
      //dead code
      builder.RegisterType<DeadCode>().InstancePerDependency();
      using var container = builder.Build();

      //WHEN
      var resolvedInstance = container.Resolve<DependencyConsumer>();

      //THEN
      Assert.NotNull(resolvedInstance);
    }

    [Test]
    public void VanillaDiContainsDeadCode()
    {
      //GIVEN
      var consumer = new DependencyConsumer(new Dependency());
      var deadCode = new DeadCode();

      //WHEN

      //THEN
      Assert.NotNull(consumer);
    }
    //bug two registrations of the same type - with different lifestyles
  }


  public record DependencyConsumer(Dependency Dependency);
  public record Dependency;
}