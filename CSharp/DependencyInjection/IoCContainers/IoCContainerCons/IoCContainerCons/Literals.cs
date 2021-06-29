using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
    public class Literals
    {
        [Test]
        public void ShouldResolveObjectWithLiterals()
        {
            //GIVEN
            var builder = new ContainerBuilder();
            builder.RegisterType<Dependency>().SingleInstance();
            builder.RegisterType<DependencyConsumer>()
                .WithParameter("X", 2).SingleInstance();
            using var container = builder.Build();

            //WHEN
            var resolvedInstance = container.Resolve<DependencyConsumer>();
            
            //THEN
            Assert.AreEqual(2, resolvedInstance.X);
        }

        [Test]
        public void ShouldResolveObjectWithLiteralsUsingVanillaDi()
        {
            //GIVEN
            var consumer = new DependencyConsumer(new Dependency(), 2);

            //WHEN
            
            //THEN
            Assert.NotNull(consumer);
        }

        public record DependencyConsumer(Dependency Dependency, int X);
        public record Dependency;
    }



}