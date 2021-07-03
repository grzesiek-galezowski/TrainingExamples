using Autofac;
using NUnit.Framework;

namespace IoCContainerPros
{
    public class AutoWiring
    {
        [Test]
        public void ShouldAutoWireDependencies()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Person>().SingleInstance();
            containerBuilder.RegisterType<Kitchen>().SingleInstance();
            containerBuilder.RegisterType<Knife>().SingleInstance();

            using (var container = containerBuilder.Build())
            {
                var person = container.Resolve<Person>();
            }
        }
    }


    public class Person
    {
        public Person(Kitchen kitchen)
        {
        }
    }

    public class Kitchen
    {
        public Kitchen(Knife knife)
        {
        }
    }

    public class Knife
    {
    }

    


}
