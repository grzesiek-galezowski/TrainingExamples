using System;
using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
  class MultipleObjectOfSameTypeConfiguredDifferentlyAndNamingPropagation
  {
    [Test]
    public void ShouldResolveTwoSimilarObjectGraphsWithDifferentLeavesFromContainer()
    {
      //GIVEN
      var builder = new ContainerBuilder();
      builder.RegisterType<World>()
        .As<World>()
        .WithParameter(
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<Character>("secondA"))
        .SingleInstance();
      
      builder.RegisterType<Character>().SingleInstance();

      builder.RegisterType<Character>()
        .WithParameter(
          (info, _) => info.Position == 0,
          (_, context) => context.ResolveNamed<Armor>("secondB1")
          )
        .WithParameter(
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<Sword>("secondB2")
          )
        .Named<Character>("secondA")
        .SingleInstance();

      builder.RegisterType<Armor>().SingleInstance();

      builder.RegisterType<Armor>()
        .WithParameter(
          (info, _) =>  info.Position == 0,
          (_, context) => context.ResolveNamed<Helmet>("secondC1"))
        .WithParameter(
          (info, _) =>  info.Position == 1,
          (_, context) => context.ResolveNamed<BreastPlate>("secondC2"))
        .Named<Armor>("secondB1")
        .SingleInstance();
      
      builder.RegisterType<Helmet>().SingleInstance();
      builder.RegisterType<Helmet>().Named<Helmet>("secondC1").SingleInstance();
      
      builder.RegisterType<BreastPlate>().WithParameter("Defense", 2).SingleInstance();
      builder.RegisterType<BreastPlate>().Named<BreastPlate>("secondC2").WithParameter("Defense", 4).SingleInstance();
      
      builder.RegisterType<Sword>().WithParameter("Attack", 4).SingleInstance();
      builder.RegisterType<Sword>().Named<Sword>("secondB2").WithParameter("Attack", 6).SingleInstance();
      using var container = builder.Build();
            
      //WHEN
      var o = container.Resolve<World>();
    
      //THEN
      Assert.AreNotSame(o.A1, o.A2);
      Assert.AreNotSame(o.A1.Armor, o.A2.Armor);
      Assert.AreNotSame(o.A1.Armor.Helmet, o.A2.Armor.Helmet);
      Assert.AreNotSame(o.A1.Armor.BreastPlate, o.A2.Armor.BreastPlate);
      Assert.AreNotSame(o.A1.Armor.BreastPlate.Defense, o.A2.Armor.BreastPlate.Defense);
      Assert.AreNotSame(o.A1.Sword, o.A2.Sword);

      Assert.AreEqual(4, o.A1.Sword.Attack);
      Assert.AreEqual(2, o.A1.Armor.BreastPlate.Defense);
      Assert.AreEqual(6, o.A2.Sword.Attack);
      Assert.AreEqual(4, o.A2.Armor.BreastPlate.Defense);
    }

    [Test]
    public void ShouldResolveTwoSimilarObjectGraphsWithDifferentLeavesFromContainerModules()
    {
      //GIVEN
      var firstCategory = Guid.NewGuid().ToString();
      var secondCategory = Guid.NewGuid().ToString();

      var builder = new ContainerBuilder();
      builder.RegisterType<World>()
        .As<World>()
        .WithParameter(
          (info, _) => info.Position == 0,
          (_, context) => context.ResolveNamed<Character>($"{firstCategory}Character"))
        .WithParameter(
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<Character>($"{secondCategory}Character"))
        .SingleInstance();

      builder.RegisterModule(new AModule(4, 2, firstCategory));
      builder.RegisterModule(new AModule(6, 4, secondCategory));

      using var container = builder.Build();

      //WHEN
      var o = container.Resolve<World>();

      //THEN
      Assert.AreNotSame(o.A1, o.A2);
      Assert.AreNotSame(o.A1.Armor, o.A2.Armor);
      Assert.AreNotSame(o.A1.Armor.Helmet, o.A2.Armor.Helmet);
      Assert.AreNotSame(o.A1.Armor.BreastPlate, o.A2.Armor.BreastPlate);
      Assert.AreNotSame(o.A1.Armor.BreastPlate.Defense, o.A2.Armor.BreastPlate.Defense);
      Assert.AreNotSame(o.A1.Sword, o.A2.Sword);

      Assert.AreEqual(4, o.A1.Sword.Attack);
      Assert.AreEqual(2, o.A1.Armor.BreastPlate.Defense);
      Assert.AreEqual(6, o.A2.Sword.Attack);
      Assert.AreEqual(4, o.A2.Armor.BreastPlate.Defense);
    }

    [Test]
    public void ShouldResolveTwoSimilarObjectGraphsWithDifferentLeavesFromVanillaDi()
    {
      var o = new World(
        new Character(
          new Armor(
            new Helmet(),
            new BreastPlate(2)),
          new Sword(4)),
        new Character(
          new Armor(
            new Helmet(),
            new BreastPlate(4)),
          new Sword(6)));

      Assert.AreNotSame(o.A1, o.A2);
      Assert.AreNotSame(o.A1.Armor, o.A2.Armor);
      Assert.AreNotSame(o.A1.Armor.Helmet, o.A2.Armor.Helmet);
      Assert.AreNotSame(o.A1.Armor.BreastPlate, o.A2.Armor.BreastPlate);
      Assert.AreNotSame(o.A1.Armor.BreastPlate.Defense, o.A2.Armor.BreastPlate.Defense);
      Assert.AreNotSame(o.A1.Sword, o.A2.Sword);

      Assert.AreEqual(4, o.A1.Sword.Attack);
      Assert.AreEqual(2, o.A1.Armor.BreastPlate.Defense);
      Assert.AreEqual(6, o.A2.Sword.Attack);
      Assert.AreEqual(4, o.A2.Armor.BreastPlate.Defense);
    }

    [Test]
    public void ShouldResolveTwoSimilarObjectGraphsWithDifferentLeavesFromVanillaDiDried()
    {
      //GIVEN
      var o = new World(
        Soldier(4, 2),
        Soldier(6, 4));

      //THEN
      Assert.AreNotSame(o.A1, o.A2);
      Assert.AreNotSame(o.A1.Armor, o.A2.Armor);
      Assert.AreNotSame(o.A1.Armor.Helmet, o.A2.Armor.Helmet);
      Assert.AreNotSame(o.A1.Armor.BreastPlate, o.A2.Armor.BreastPlate);
      Assert.AreNotSame(o.A1.Armor.BreastPlate.Defense, o.A2.Armor.BreastPlate.Defense);
      Assert.AreNotSame(o.A1.Sword, o.A2.Sword);

      Assert.AreEqual(4, o.A1.Sword.Attack);
      Assert.AreEqual(2, o.A1.Armor.BreastPlate.Defense);
      Assert.AreEqual(6, o.A2.Sword.Attack);
      Assert.AreEqual(4, o.A2.Armor.BreastPlate.Defense);
    }

    private static Character Soldier(int x1, int x2)
    {
      return new Character(
        new Armor(
          new Helmet(),
          new BreastPlate(x2)),
        new Sword(x1));
    }

    public record World(Character A1, Character A2);
    public record Character(Armor Armor, Sword Sword);
    public record Armor(Helmet Helmet, BreastPlate BreastPlate);
    public record BreastPlate(int Defense);
    public record Helmet;
    public record Sword(int Attack);



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
        builder.RegisterType<Character>()
          .WithParameter(
            (info, _) => info.Position == 0,
            (_, context) => context.ResolveNamed<Armor>($"{_category}Armor")
          )
          .WithParameter(
            (info, _) => info.Position == 1,
            (_, context) => context.ResolveNamed<Sword>($"{_category}Sword")
          )
          .Named<Character>($"{_category}Character")
          .SingleInstance();

        builder.RegisterType<Armor>()
          .WithParameter(
            (info, _) => info.Position == 0,
            (_, context) => context.ResolveNamed<Helmet>($"{_category}Helmet"))
          .WithParameter(
            (info, _) => info.Position == 1,
            (_, context) => context.ResolveNamed<BreastPlate>($"{_category}BreastPlate"))
          .Named<Armor>($"{_category}Armor").SingleInstance();

        builder.RegisterType<Sword>().Named<Sword>($"{_category}Sword").SingleInstance().WithParameter("Attack", _x1);
        builder.RegisterType<Helmet>().Named<Helmet>($"{_category}Helmet").SingleInstance();
        builder.RegisterType<BreastPlate>().Named<BreastPlate>($"{_category}BreastPlate").SingleInstance().WithParameter("Defense", _x2);

      }
    }
  }




}
