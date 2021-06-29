using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
  public class CaptiveDependency
  {
    [Test]
    public void ShouldShowCaptiveDependencyIssue()
    {
      var i = 0;
      //GIVEN
      var builder = new ContainerBuilder();
      builder.Register(ctr => "Lol" + i++).InstancePerLifetimeScope();
      builder.RegisterType<Captor>().SingleInstance();
      using var container = builder.Build();

      //WHEN
      using (var scope = container.BeginLifetimeScope())
      {
        var text1 = scope.Resolve<string>();
        var text2 = scope.Resolve<string>();
        var captor1 = scope.Resolve<Captor>();
        var captor2 = scope.Resolve<Captor>();
        //THEN
        Assert.AreEqual("Lol0", text1);
        Assert.AreEqual("Lol0", text2);
        Assert.AreEqual("Lol1", captor1.Str);
        Assert.AreEqual("Lol1", captor2.Str);
      }

      using (var scope = container.BeginLifetimeScope())
      {
        var text1 = scope.Resolve<string>();
        var text2 = scope.Resolve<string>();
        var captor1 = scope.Resolve<Captor>();
        var captor2 = scope.Resolve<Captor>();
        //THEN
        Assert.AreEqual("Lol2", text1);
        Assert.AreEqual("Lol2", text2);
        Assert.AreEqual("Lol1", captor1.Str);
        Assert.AreEqual("Lol1", captor2.Str);
      }

      using (var scope = container.BeginLifetimeScope())
      {
        var text1 = scope.Resolve<string>();
        var text2 = scope.Resolve<string>();
        var captor1 = scope.Resolve<Captor>();
        var captor2 = scope.Resolve<Captor>();
        //THEN
        Assert.AreEqual("Lol3", text1);
        Assert.AreEqual("Lol3", text2);
        Assert.AreEqual("Lol1", captor1.Str);
        Assert.AreEqual("Lol1", captor2.Str);
      }
    }

    public record Captor(string Str);
  }

}