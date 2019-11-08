using System;
using Autofac;
using NUnit.Framework;

namespace IoCContainerCons
{
    public class EventsAndOnActivatedOnReleased
    {
        [Test]
        public void ShouldShowHandMadeHandlingOfEvents()
        {
            //GIVEN
            var builder = new ContainerBuilder();
            builder.RegisterType<MyObserver>().SingleInstance();
            builder.RegisterType<MyDependency>().InstancePerDependency()
                .OnActivated(args => 
                    args.Instance.SomeKindOfEvent += args.Context.Resolve<MyObserver>().Notify);
            using var container = builder.Build();
            
            //WHEN
            var observer = container.Resolve<MyObserver>();
            var dependency1 = container.Resolve<MyDependency>();
            var dependency2 = container.Resolve<MyDependency>();
            var dependency3 = container.Resolve<MyDependency>();

            //THEN
            dependency1.DoSomething();
            Assert.AreEqual(dependency1.InstanceId, observer.LastReceived);

            dependency2.DoSomething();
            Assert.AreEqual(dependency2.InstanceId, observer.LastReceived);

            dependency3.DoSomething();
            Assert.AreEqual(dependency3.InstanceId, observer.LastReceived);
        }

        [Test]
        public void ShouldShowHandMadeHandlingOfEventsWithManualComposition()
        {
            //GIVEN
            var observer = new MyObserver();
            var dependency1 = new MyDependency();
            var dependency2 = new MyDependency();
            var dependency3 = new MyDependency();

            //WHEN
            dependency1.SomeKindOfEvent += observer.Notify;
            dependency2.SomeKindOfEvent += observer.Notify;
            dependency3.SomeKindOfEvent += observer.Notify;

            //THEN
            dependency1.DoSomething();
            Assert.AreEqual(dependency1.InstanceId, observer.LastReceived);

            dependency2.DoSomething();
            Assert.AreEqual(dependency2.InstanceId, observer.LastReceived);

            dependency3.DoSomething();
            Assert.AreEqual(dependency3.InstanceId, observer.LastReceived);
        }
    }

    public class MyObserver
    {
        public void Notify(int value)
        {
            LastReceived = value;
        }

        public int LastReceived { get; set; }
    }

    public class MyDependency
    {
        private static int _lastInstanceId = 0;
        public int InstanceId { get; }

        public MyDependency()
        {
            InstanceId = _lastInstanceId++;
        }

        public event Action<int> SomeKindOfEvent;

        public void DoSomething()
        {
            SomeKindOfEvent?.Invoke(InstanceId);
        }
    }
}
