using Autofac;
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
                    new Answer()));
            
            Assert.IsInstanceOf<SynchronizedAnswer>(answer);
            Assert.IsInstanceOf<TracedAnswer>(answer.NestedAnswer);
            Assert.IsInstanceOf<Answer>(answer.NestedAnswer.NestedAnswer);
        }

        [Test]
        public void ShouldAssembleDecoratorsUsingContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Answer>().As<IAnswer>();
            builder.RegisterDecorator<TracedAnswer, IAnswer>();
            builder.RegisterDecorator<SynchronizedAnswer, IAnswer>();
            //nothing more that can be done with it, e.g. cannot pass NamedParameter

            using var container = builder.Build();
            var answer = container.Resolve<IAnswer>();
            Assert.IsInstanceOf<SynchronizedAnswer>(answer);
            Assert.IsInstanceOf<TracedAnswer>(answer.NestedAnswer);
            Assert.IsInstanceOf<Answer>(answer.NestedAnswer.NestedAnswer);

        }
    }

    public class TracedAnswer : IAnswer
    {
        public IAnswer NestedAnswer { get; }

        public TracedAnswer(IAnswer answer)
        {
            NestedAnswer = answer;
        }
    }

    public class SynchronizedAnswer : IAnswer
    {
        public IAnswer NestedAnswer { get; }

        public SynchronizedAnswer(IAnswer answer)
        {
            NestedAnswer = answer;
        }
    }

    public interface IAnswer
    {
        IAnswer NestedAnswer { get; }
    }

    public class Answer : IAnswer
    {
        public IAnswer NestedAnswer => null;
    }
}
