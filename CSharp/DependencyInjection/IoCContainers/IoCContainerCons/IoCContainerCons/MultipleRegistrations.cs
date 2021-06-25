using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
  class MultipleRegistrations
  {
    [Test]
    public void ShouldResolveLastRegisteredImplementationFromContainer()
    {
      //GIVEN
      var builder = new ContainerBuilder();
      builder.RegisterType<Constructor1Argument>().As<ConstructorArgument>().SingleInstance();
      builder.RegisterType<Constructor2Argument>().As<ConstructorArgument>().SingleInstance();
      builder.RegisterType<ObjectWithConstructorArgument>().SingleInstance();
      using var container = builder.Build();
            
      //WHEN
      var resolvedInstance = container.Resolve<ObjectWithConstructorArgument>();

      //THEN
      Assert.IsInstanceOf<Constructor2Argument>(resolvedInstance._arg);
    }
    
    
    [Test]
    public void ShouldResolveLastRegisteredImplementationFromContainerForTheSameTypeAndDifferentLifestyles()
    {
      //GIVEN
      var builder = new ContainerBuilder();
      builder.Register<Constructor1Argument>(context =>
      {
        Assert.Fail("should not be called");
        return null;
      }).As<ConstructorArgument>().SingleInstance();
      builder.RegisterType<Constructor1Argument>().As<ConstructorArgument>().InstancePerDependency();
      builder.RegisterType<ObjectWithConstructorArgument>().SingleInstance();
      using var container = builder.Build();
            
      //WHEN
      var resolvedInstance = container.Resolve<ObjectWithConstructorArgument>();

      //THEN
      Assert.IsInstanceOf<Constructor1Argument>(resolvedInstance._arg);
    }



    public class ObjectWithConstructorArgument
    {
      public readonly ConstructorArgument _arg;

      public ObjectWithConstructorArgument(ConstructorArgument arg)
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


}
