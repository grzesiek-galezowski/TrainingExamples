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
      builder.RegisterType<O>()
        .As<O>()
        .WithParameter(
          (info, context) => info.Position == 0,
          (info, context) => context.ResolveNamed<A>("firstA"))
        .WithParameter(
          (info, context) => info.Position == 1,
          (info, context) => context.ResolveNamed<A>("secondA"))
        .SingleInstance();
      
      builder.RegisterType<A>().As<A>()
        .WithParameter(
          (info, context) => info.Position == 0,
          (info, context) => context.ResolveNamed<B1>("firstB1")
        )
        .WithParameter(
          (info, context) => info.Position == 1,
          (info, context) => context.ResolveNamed<B2>("firstB2")
        )
        .Named<A>("firstA")
        .SingleInstance();

      builder.RegisterType<A>().As<A>()
        .WithParameter(
          (info, context) => info.Position == 0,
          (info, context) => context.ResolveNamed<B1>("secondB1")
          )
        .WithParameter(
          (info, context) => info.Position == 1,
          (info, context) => context.ResolveNamed<B2>("secondB2")
          )
        .Named<A>("secondA")
        .SingleInstance();

      builder.RegisterType<B1>()        
        .WithParameter(
          (info, _) =>  info.Position == 0,
          (_, context) => context.ResolveNamed<C1>("firstC1"))
        .WithParameter(
          (info, _) =>  info.Position == 1,
          (_, context) => context.ResolveNamed<C2>("firstC2"))
        .As<B1>().Named<B1>("firstB1").SingleInstance();

      builder.RegisterType<B1>()
        .WithParameter(
          (info, _) =>  info.Position == 0,
          (_, context) => context.ResolveNamed<C1>("secondC1"))
        .WithParameter(
          (info, _) =>  info.Position == 1,
          (_, context) => context.ResolveNamed<C2>("secondC2"))
        .As<B1>().Named<B1>("secondB1").SingleInstance();
      
      builder.RegisterType<C1>().Named<C1>("firstC1").SingleInstance();
      builder.RegisterType<C1>().Named<C1>("secondC1").SingleInstance();
      
      builder.RegisterType<C2>().Named<C2>("firstC2").SingleInstance().WithParameter("X2", 2);
      builder.RegisterType<C2>().Named<C2>("secondC2").WithParameter("X2", 4).SingleInstance();
      
      builder.RegisterType<B2>().Named<B2>("firstB2").SingleInstance().WithParameter("X1", 4);
      builder.RegisterType<B2>().Named<B2>("secondB2").WithParameter("X1", 6).SingleInstance();
      using var container = builder.Build();
            
      //WHEN
      var o = container.Resolve<O>();
      
    
      //THEN
      Assert.AreNotSame(o.A1, o.A2);
      Assert.AreNotSame(o.A1.B1, o.A2.B1);
      Assert.AreNotSame(o.A1.B1.C1, o.A2.B1.C1);
      Assert.AreNotSame(o.A1.B1.C2, o.A2.B1.C2);
      Assert.AreNotSame(o.A1.B1.C2.X2, o.A2.B1.C2.X2);
      Assert.AreNotSame(o.A1.B2, o.A2.B2);

      Assert.AreEqual(4, o.A1.B2.X1);
      Assert.AreEqual(2, o.A1.B1.C2.X2);
      Assert.AreEqual(6, o.A2.B2.X1);
      Assert.AreEqual(4, o.A2.B1.C2.X2);
    }

    [Test]
    public void ShouldXXXXXXXXXXXXXXXXXXXXXXX() //bug
    {
      //GIVEN
      //WHEN
      var o = new O(
        new A(
          new B1(
            new C1(),
            new C2(2)),
          new B2(4)),
        new A(
          new B1(
            new C1(),
            new C2(4)),
          new B2(6)));

      //THEN
      Assert.AreEqual(4, o.A1.B2.X1);
      Assert.AreEqual(2, o.A1.B1.C2.X2);
      Assert.AreEqual(6, o.A2.B2.X1);
      Assert.AreEqual(4, o.A2.B1.C2.X2);
    }

    public record O(A A1, A A2);
    public record A(B1 B1, B2 B2);
    public record B1(C1 C1, C2 C2);
    public record C2(int X2);
    public record C1;
    public record B2(int X1);
  }


}
