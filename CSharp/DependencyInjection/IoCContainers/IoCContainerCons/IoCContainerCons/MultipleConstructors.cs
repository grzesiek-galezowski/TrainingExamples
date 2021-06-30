using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
  class MultipleConstructors
  {
    [Test]
    public void ShouldResolveUsingFirstConstructorFromContainer()
    {
      //GIVEN
      var builder = new ContainerBuilder();
      builder.RegisterType<Constructor1Argument>().SingleInstance();
      builder.RegisterType<Constructor2Argument>().SingleInstance();
      builder.RegisterType<ObjectWithTwoConstructors>()
        .UsingConstructor(typeof(Constructor1Argument)).SingleInstance();
      using var container = builder.Build();
            
      //WHEN
      var resolvedInstance = container.Resolve<ObjectWithTwoConstructors>();

      //THEN
      Assert.IsInstanceOf<Constructor1Argument>(resolvedInstance.Arg);
    }

    [Test]
    public void ShouldResolveUsingFirstConstructorUsingVanillaDi()
    {
      //GIVEN

      //WHEN
      var resolvedInstance = new ObjectWithTwoConstructors(new Constructor1Argument());

      //THEN
      Assert.IsInstanceOf<Constructor1Argument>(resolvedInstance.Arg);
    }

    public class ObjectWithTwoConstructors
    {
      public readonly ConstructorArgument Arg;

      public ObjectWithTwoConstructors(Constructor1Argument arg)
      {
        Arg = arg;
      }
      public ObjectWithTwoConstructors(Constructor2Argument arg)
      {
        Arg = arg;
      }
    }

    public interface ConstructorArgument
    {

    }

    public record Constructor1Argument : ConstructorArgument;
    public record Constructor2Argument : ConstructorArgument;
  }


}
