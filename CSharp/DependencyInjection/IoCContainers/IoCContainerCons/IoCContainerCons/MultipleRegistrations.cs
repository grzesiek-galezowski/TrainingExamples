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
      Assert.IsInstanceOf<Constructor2Argument>(resolvedInstance.Arg);
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
      Assert.IsInstanceOf<Constructor1Argument>(resolvedInstance.Arg);
    }



    public record ObjectWithConstructorArgument(ConstructorArgument Arg);

    public interface ConstructorArgument { }
    public record Constructor1Argument : ConstructorArgument;
    public record Constructor2Argument : ConstructorArgument;
  }


}
