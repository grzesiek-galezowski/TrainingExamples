using System;
using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
  class MultipleObjectOfSameTypeConfiguredDifferentlyAndNamingPropagation
  {
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

      Assert.AreNotSame(o.Character1, o.Character2);
      Assert.AreNotSame(o.Character1.Armor, o.Character2.Armor);
      Assert.AreNotSame(o.Character1.Armor.Helmet, o.Character2.Armor.Helmet);
      Assert.AreNotSame(o.Character1.Armor.BreastPlate, o.Character2.Armor.BreastPlate);
      Assert.AreNotSame(o.Character1.Armor.BreastPlate.Defense, o.Character2.Armor.BreastPlate.Defense);
      Assert.AreNotSame(o.Character1.Sword, o.Character2.Sword);

      Assert.AreEqual(4, o.Character1.Sword.Attack);
      Assert.AreEqual(2, o.Character1.Armor.BreastPlate.Defense);
      Assert.AreEqual(6, o.Character2.Sword.Attack);
      Assert.AreEqual(4, o.Character2.Armor.BreastPlate.Defense);
    }

    [Test]
    public void ShouldResolveTwoSimilarObjectGraphsWithDifferentLeavesFromVanillaDiDried()
    {
      //GIVEN
      var o = new World(
        Soldier(4, 2),
        Soldier(6, 4));

      //THEN
      Assert.AreNotSame(o.Character1, o.Character2);
      Assert.AreNotSame(o.Character1.Armor, o.Character2.Armor);
      Assert.AreNotSame(o.Character1.Armor.Helmet, o.Character2.Armor.Helmet);
      Assert.AreNotSame(o.Character1.Armor.BreastPlate, o.Character2.Armor.BreastPlate);
      Assert.AreNotSame(o.Character1.Armor.BreastPlate.Defense, o.Character2.Armor.BreastPlate.Defense);
      Assert.AreNotSame(o.Character1.Sword, o.Character2.Sword);

      Assert.AreEqual(4, o.Character1.Sword.Attack);
      Assert.AreEqual(2, o.Character1.Armor.BreastPlate.Defense);
      Assert.AreEqual(6, o.Character2.Sword.Attack);
      Assert.AreEqual(4, o.Character2.Armor.BreastPlate.Defense);
    }


    [Test]
    public void ShouldResolveTwoSimilarObjectGraphsWithDifferentLeavesFromContainer()
    {
      //GIVEN
      var builder = new ContainerBuilder();
      builder.RegisterType<World>()
        .As<World>()
        .WithParameter(
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<Character>("secondCharacter"))
        .SingleInstance();

      builder.RegisterType<Character>().SingleInstance();

      builder.RegisterType<Character>()
        .WithParameter(
          (info, _) => info.Position == 0,
          (_, context) => context.ResolveNamed<Armor>("secondArmor")
        )
        .WithParameter(
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<Sword>("secondSword")
        )
        .Named<Character>("secondCharacter")
        .SingleInstance();

      builder.RegisterType<Armor>().SingleInstance();

      builder.RegisterType<Armor>()
        .WithParameter(
          (info, _) => info.Position == 1,
          (_, context) => context.ResolveNamed<BreastPlate>("secondBreastPlate"))
        .Named<Armor>("secondArmor")
        .SingleInstance();

      builder.RegisterType<Helmet>().InstancePerDependency();

      builder.RegisterType<BreastPlate>()
        .WithParameter("Defense", 2)
        .SingleInstance();
      builder.RegisterType<BreastPlate>()
        .Named<BreastPlate>("secondBreastPlate")
        .WithParameter("Defense", 4).SingleInstance();

      builder.RegisterType<Sword>()
        .WithParameter("Attack", 4)
        .SingleInstance();
      builder.RegisterType<Sword>()
        .Named<Sword>("secondSword")
        .WithParameter("Attack", 6).SingleInstance();
      using var container = builder.Build();

      //WHEN
      var o = container.Resolve<World>();

      //THEN
      Assert.AreNotSame(o.Character1, o.Character2);
      Assert.AreNotSame(o.Character1.Armor, o.Character2.Armor);
      Assert.AreNotSame(o.Character1.Armor.Helmet, o.Character2.Armor.Helmet);
      Assert.AreNotSame(o.Character1.Armor.BreastPlate, o.Character2.Armor.BreastPlate);
      Assert.AreNotSame(o.Character1.Armor.BreastPlate.Defense, o.Character2.Armor.BreastPlate.Defense);
      Assert.AreNotSame(o.Character1.Sword, o.Character2.Sword);

      Assert.AreEqual(4, o.Character1.Sword.Attack);
      Assert.AreEqual(2, o.Character1.Armor.BreastPlate.Defense);
      Assert.AreEqual(6, o.Character2.Sword.Attack);
      Assert.AreEqual(4, o.Character2.Armor.BreastPlate.Defense);
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

      builder.RegisterModule(new SoldierModule(4, 2, firstCategory));
      builder.RegisterModule(new SoldierModule(6, 4, secondCategory));

      using var container = builder.Build();

      //WHEN
      var o = container.Resolve<World>();

      //THEN
      Assert.AreNotSame(o.Character1, o.Character2);
      Assert.AreNotSame(o.Character1.Armor, o.Character2.Armor);
      Assert.AreNotSame(o.Character1.Armor.Helmet, o.Character2.Armor.Helmet);
      Assert.AreNotSame(o.Character1.Armor.BreastPlate, o.Character2.Armor.BreastPlate);
      Assert.AreNotSame(o.Character1.Armor.BreastPlate.Defense, o.Character2.Armor.BreastPlate.Defense);
      Assert.AreNotSame(o.Character1.Sword, o.Character2.Sword);

      Assert.AreEqual(4, o.Character1.Sword.Attack);
      Assert.AreEqual(2, o.Character1.Armor.BreastPlate.Defense);
      Assert.AreEqual(6, o.Character2.Sword.Attack);
      Assert.AreEqual(4, o.Character2.Armor.BreastPlate.Defense);
    }

    private static Character Soldier(int x1, int x2)
    {
      return new Character(
        new Armor(
          new Helmet(),
          new BreastPlate(x2)),
        new Sword(x1));
    }

    public record World(Character Character1, Character Character2);

    public record Character(Armor Armor, Sword Sword);

    public record Armor(Helmet Helmet, BreastPlate BreastPlate);

    public record BreastPlate(int Defense);

    public record Helmet;

    public record Sword(int Attack);

    public class SoldierModule : Module
    {
      private readonly int _swordAttack;
      private readonly int _breastplateDefense;
      private readonly string _category;

      public SoldierModule(int swordAttack, int breastplateDefense, string category)
      {
        _swordAttack = swordAttack;
        _breastplateDefense = breastplateDefense;
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
          .Named<Armor>($"{_category}Armor")
          .SingleInstance();

        builder.RegisterType<Sword>()
          .Named<Sword>($"{_category}Sword")
          .WithParameter("Attack", _swordAttack)
          .SingleInstance();
        builder.RegisterType<Helmet>()
          .Named<Helmet>($"{_category}Helmet")
          .SingleInstance();
        builder.RegisterType<BreastPlate>()
          .Named<BreastPlate>($"{_category}BreastPlate")
          .WithParameter("Defense", _breastplateDefense)
          .SingleInstance();

      }
    }
  }
}