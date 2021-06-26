using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
  class MultipleObjectOfSameTypeConfiguredDifferentlyAndNamingPropagation
  {
    [Test]
    public void ShouldXXXXXXXXXXXXXXXX() //bug
    {
      //GIVEN
      var builder = new ContainerBuilder();
      builder.RegisterType<A>().SingleInstance();
      builder.RegisterType<A>().SingleInstance();
      builder.RegisterType<B1>().SingleInstance();
      builder.RegisterType<B1>().SingleInstance();
      builder.RegisterType<C1>().SingleInstance();
      builder.RegisterType<C1>().SingleInstance();
      builder.RegisterType<C2>().SingleInstance();
      builder.RegisterType<C2>().SingleInstance();
      builder.RegisterType<B2>().SingleInstance();
      builder.RegisterType<B2>().SingleInstance();
      using var container = builder.Build();
            
      //WHEN
      var a1 = container.Resolve<A>();
    
      //THEN
      Assert.AreEqual(4, a1.B2.X1);
      Assert.AreEqual(2, a1.B1.C2.X2);
      //bug Assert.AreEqual(6, a2.B2.X1);
      //bug Assert.AreEqual(4, a2.B1.C2.X2);
    }

    [Test]
    public void ShouldXXXXXXXXXXXXXXXXXXXXXXX() //bug
    {
      //GIVEN
      //WHEN
      var a1 = new A(new B1(new C1(), new C2(2)), new B2(4));
      var a2 = new A(new B1(new C1(), new C2(4)), new B2(6));

      //THEN
      Assert.AreEqual(4, a1.B2.X1);
      Assert.AreEqual(2, a1.B1.C2.X2);
      Assert.AreEqual(6, a2.B2.X1);
      Assert.AreEqual(4, a2.B1.C2.X2);
    }

    public record A(B1 B1, B2 B2);
    public record B1(C1 C1, C2 C2);
    public record C2(int X2);
    public record C1;
    public record B2(int X1);
  }


}
