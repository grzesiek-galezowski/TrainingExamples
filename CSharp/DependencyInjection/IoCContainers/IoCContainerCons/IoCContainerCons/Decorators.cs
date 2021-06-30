using Autofac;
using Autofac.Features.Decorators;
using NUnit.Framework;

//CONS:
//1. containers take time to learn (every special case -> new feature)
//2. containers hide the dependency graph
//3. containers do not yield themselves to refactoring
//4. containers give away some of the compile time checks

namespace IoCContainerCons
{
  public class Decorators
  {
    //TODO passing part of the chain to one object and full chain to another
    [Test]
    public void ShouldAssembleDecoratorsByHand()
    {
      var answer = new SynchronizedAnswer(
        new TracedAnswer(
          new Answer()), 
        1);

      Assert.IsInstanceOf<SynchronizedAnswer>(answer);
      Assert.IsInstanceOf<TracedAnswer>(answer.NestedAnswer);
      Assert.IsInstanceOf<Answer>(answer.NestedAnswer.NestedAnswer);
      Assert.AreEqual(1, answer.X);
    }

    [Test]
    public void ShouldAssembleDecoratorsUsingContainer()
    {
      var builder = new ContainerBuilder();

      builder.RegisterType<Answer>().As<IAnswer>();
      builder.RegisterDecorator<TracedAnswer, IAnswer>();
      //To use .WithParameter(), do this:
      builder.RegisterType<SynchronizedAnswer>()
        .As(new DecoratorService(typeof(IAnswer)))
        .WithParameter("X", 1);

      using var container = builder.Build();
      var answer = container.Resolve<IAnswer>();
      Assert.IsInstanceOf<SynchronizedAnswer>(answer);
      Assert.IsInstanceOf<TracedAnswer>(answer.NestedAnswer);
      Assert.IsInstanceOf<Answer>(answer.NestedAnswer.NestedAnswer);
      Assert.AreEqual(1, ((SynchronizedAnswer)answer).X);
    }

    public interface IAnswer
    {
      IAnswer NestedAnswer { get; }
    }

    public record TracedAnswer(IAnswer NestedAnswer) : IAnswer;
    public record SynchronizedAnswer(IAnswer NestedAnswer, int X) : IAnswer;
    public record Answer : IAnswer
    {
      public IAnswer NestedAnswer => null;
    }

  }


}
