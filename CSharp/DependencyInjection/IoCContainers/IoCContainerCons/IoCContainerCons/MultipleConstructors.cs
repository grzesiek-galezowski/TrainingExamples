using System;
using System.Collections.Generic;
using System.Text;
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
      Assert.IsInstanceOf<Constructor1Argument>(resolvedInstance._arg);
    }

    [Test]
    public void ShouldResolveUsingFirstConstructorUsingVanillaDi()
    {
      //GIVEN

      //WHEN
      var resolvedInstance = new ObjectWithTwoConstructors(new Constructor1Argument());

      //THEN
      Assert.IsInstanceOf<Constructor1Argument>(resolvedInstance._arg);
    }

  }

  public class ObjectWithTwoConstructors
  {
    public readonly ConstructorArgument _arg;

    public ObjectWithTwoConstructors(Constructor1Argument arg)
    {
      _arg = arg;
    }
    public ObjectWithTwoConstructors(Constructor2Argument arg)
    {
      _arg = arg;
    }
  }

  public interface ConstructorArgument
  {

  }

  public class Constructor1Argument : ConstructorArgument
  {
  }

  public class Constructor2Argument : ConstructorArgument
  {
  }
}
