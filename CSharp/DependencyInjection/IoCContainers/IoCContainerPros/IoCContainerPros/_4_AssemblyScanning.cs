using System.Reflection;
using Autofac;
using Autofac.Core.Registration;
using NUnit.Framework;

namespace IoCContainerPros
{
  public class AssemblyScanning
  {
    [Test]
    public void ShouldBeAbleToResolveBasedOnConvention()
    {
      var builder = new ContainerBuilder();

      builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
        .Where(t => t.Name.EndsWith("Repository"))
        .AsImplementedInterfaces().SingleInstance();

      using (var container = builder.Build())
      {
        var i1 = container.Resolve<Interface1>();
        var i2 = container.Resolve<Interface2>();

        Assert.IsInstanceOf<MyRepository>(i1);
        Assert.IsInstanceOf<MyRepository>(i2);
        Assert.AreEqual(i2, i1);
        
        Assert.Throws<ComponentNotRegisteredException>(
          () => container.Resolve<MyRepository2>()); //not following convention
      }
    }
  }

  public interface Interface1
  {

  }
  public interface Interface2
  {

  }

  public class MyRepository : Interface1, Interface2
  {

  }

  public class MyRepository2 : Interface1, Interface2
  {

  }

}
