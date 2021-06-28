using System;
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
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<A>("secondA"))
        .SingleInstance();
      
      builder.RegisterType<A>().SingleInstance();

      builder.RegisterType<A>()
        .WithParameter(
          (info, _) => info.Position == 0,
          (_, context) => context.ResolveNamed<B1>("secondB1")
          )
        .WithParameter(
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<B2>("secondB2")
          )
        .Named<A>("secondA")
        .SingleInstance();

      builder.RegisterType<B1>().SingleInstance();

      builder.RegisterType<B1>()
        .WithParameter(
          (info, _) =>  info.Position == 0,
          (_, context) => context.ResolveNamed<C1>("secondC1"))
        .WithParameter(
          (info, _) =>  info.Position == 1,
          (_, context) => context.ResolveNamed<C2>("secondC2"))
        .Named<B1>("secondB1")
        .SingleInstance();
      
      builder.RegisterType<C1>().SingleInstance();
      builder.RegisterType<C1>().Named<C1>("secondC1").SingleInstance();
      
      builder.RegisterType<C2>().WithParameter("X2", 2).SingleInstance();
      builder.RegisterType<C2>().Named<C2>("secondC2").WithParameter("X2", 4).SingleInstance();
      
      builder.RegisterType<B2>().WithParameter("X1", 4).SingleInstance();
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
    public void ShouldXXXXXXXXXXXXXXXX2() //bug
    {
      //GIVEN
      var firstCategory = Guid.NewGuid().ToString();
      var secondCategory = Guid.NewGuid().ToString();

      var builder = new ContainerBuilder();
      builder.RegisterType<O>()
        .As<O>()
        .WithParameter(
          (info, _) => info.Position == 0,
          (_, context) => context.ResolveNamed<A>($"{firstCategory}A"))
        .WithParameter(
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<A>($"{secondCategory}A"))
        .SingleInstance();

      builder.RegisterModule(new AModule(4, 2, firstCategory));
      builder.RegisterModule(new AModule(6, 4, secondCategory));

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

    //bug add version refactored to modules

    [Test]
    public void ShouldXXXXXXXXXXXXXXXXXXXXXXX() //bug
    {
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
    public void ShouldXXXXXXXXXXXXXXXXXXXXXXX2() //bug
    {
      //bug add version refactored to methods
      //GIVEN
      //WHEN
      var o = new O(
        CreateA(4, 2),
        CreateA(6, 4));

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

    private static A CreateA(int x1, int x2)
    {
      return new A(
        new B1(
          new C1(),
          new C2(x2)),
        new B2(x1));
    }

    public record O(A A1, A A2);
    public record A(B1 B1, B2 B2);
    public record B1(C1 C1, C2 C2);
    public record C2(int X2);
    public record C1;
    public record B2(int X1);



    public class AModule : Module
    {
      private readonly int _x1;
      private readonly int _x2;
      private readonly string _category;

      public AModule(int x1, int x2, string category)
      {
        _x1 = x1;
        _x2 = x2;
        _category = category;
      }

      protected override void Load(ContainerBuilder builder)
      {
        builder.RegisterType<A>()
          .WithParameter(
            (info, _) => info.Position == 0,
            (_, context) => context.ResolveNamed<B1>($"{_category}B1")
          )
          .WithParameter(
            (info, _) => info.Position == 1,
            (_, context) => context.ResolveNamed<B2>($"{_category}B2")
          )
          .Named<A>($"{_category}A")
          .SingleInstance();

        builder.RegisterType<B1>()
          .WithParameter(
            (info, _) => info.Position == 0,
            (_, context) => context.ResolveNamed<C1>($"{_category}C1"))
          .WithParameter(
            (info, _) => info.Position == 1,
            (_, context) => context.ResolveNamed<C2>($"{_category}C2"))
          .Named<B1>($"{_category}B1").SingleInstance();

        builder.RegisterType<B2>().Named<B2>($"{_category}B2").SingleInstance().WithParameter("X1", _x1);
        builder.RegisterType<C1>().Named<C1>($"{_category}C1").SingleInstance();
        builder.RegisterType<C2>().Named<C2>($"{_category}C2").SingleInstance().WithParameter("X2", _x2);

      }
    }
  }




}
