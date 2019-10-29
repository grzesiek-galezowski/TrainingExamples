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
            builder.RegisterType<MyMonitor>()
                .SingleInstance();
            builder.RegisterType<MyDependency>()
                .InstancePerDependency()
                .OnActivated(args => 
                    args.Instance.SomeKindOfEvent += 
                        args.Context.Resolve<MyMonitor>().Notify);
            using var container = builder.Build();
            
            //WHEN
            var monitor = container.Resolve<MyMonitor>();
            var dependency1 = container.Resolve<MyDependency>();
            var dependency2 = container.Resolve<MyDependency>();
            var dependency3 = container.Resolve<MyDependency>();

            //THEN
            dependency1.DoSomething();
            Assert.AreEqual(dependency1.InstanceId, monitor.LastReceived);

            dependency2.DoSomething();
            Assert.AreEqual(dependency2.InstanceId, monitor.LastReceived);

            dependency3.DoSomething();
            Assert.AreEqual(dependency3.InstanceId, monitor.LastReceived);
        }

        [Test]
        public void ShouldShowHandMadeHandlingOfEventsWithManualComposition()
        {
            //GIVEN
            var builder = new ContainerBuilder();
            builder.RegisterType<MyMonitor>()
                .SingleInstance();
            builder.RegisterType<MyDependency>()
                .InstancePerDependency()
                .OnActivated(args => 
                    args.Instance.SomeKindOfEvent += 
                        args.Context.Resolve<MyMonitor>().Notify);
            using var container = builder.Build();
            
            //WHEN
            var monitor = new MyMonitor();
            var dependency1 = new MyDependency();
            dependency1.SomeKindOfEvent += monitor.Notify;
            
            var dependency2 = new MyDependency();
            dependency2.SomeKindOfEvent += monitor.Notify;
            
            var dependency3 = new MyDependency();
            dependency3.SomeKindOfEvent += monitor.Notify;

            //THEN
            dependency1.DoSomething();
            Assert.AreEqual(dependency1.InstanceId, monitor.LastReceived);

            dependency2.DoSomething();
            Assert.AreEqual(dependency2.InstanceId, monitor.LastReceived);

            dependency3.DoSomething();
            Assert.AreEqual(dependency3.InstanceId, monitor.LastReceived);
        }

    }

    public class MyMonitor
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
