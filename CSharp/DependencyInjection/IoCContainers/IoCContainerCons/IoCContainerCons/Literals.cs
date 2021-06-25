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
                .WithParameter("x", 2).SingleInstance();
            using var container = builder.Build();

            //WHEN
            var resolvedInstance = container.Resolve<DependencyConsumer>();
            
            //THEN
            Assert.AreEqual(2, resolvedInstance._x);
        }

        [Test]
        public void VanillaDiContainsDeadCode()
        {
            //GIVEN
            var consumer = new DependencyConsumer(new Dependency(), 2);
            var deadCode = new DeadCode();

            //WHEN
            
            //THEN
            Assert.NotNull(consumer);
        }
        
        public class DependencyConsumer
        {
            public readonly int _x;

            public DependencyConsumer(Dependency dependency, int x)
            {
                _x = x;
            }
        }

        public class Dependency
        {
        }
    }



}