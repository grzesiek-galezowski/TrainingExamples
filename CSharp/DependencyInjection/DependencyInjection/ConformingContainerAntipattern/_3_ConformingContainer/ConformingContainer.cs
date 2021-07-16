using System;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace ConformingContainerAntipattern._3_ConformingContainer
{
  public class ConformingContainer : IDisposable
  {
    readonly IUnityContainer _container = new UnityContainer();

    public For<T> For<T>()
    {
      return new For<T>(_container, this);
    }

    public void UseAlwaysTheSame<T>()
    {
      For<T>().UseAlwaysTheSame<T>();
    }

    public void UseEachTimeNew<T>()
    {
      For<T>().UseEachTimeNew<T>();
    }

    public void Dispose()
    {
      _container.Dispose();
    }

    public T Resolve<T>()
    {
      return _container.Resolve<T>();
    }
  }

  public class For<T>
  {
    private readonly IUnityContainer _container;
    private readonly ConformingContainer _conformingContainer;

    public For(IUnityContainer container, ConformingContainer conformingContainer)
    {
      _container = container;
      _conformingContainer = conformingContainer;
    }

    public void UseAlwaysTheSame<U>() where U : T
    {
      _container.RegisterType<T, U>(new ContainerControlledLifetimeManager());
    }

    public void UseEachTimeNew<U>() where U : T
    {
      _container.RegisterType<T, U>();
    }

    public void UseInstancesCreatedWith(Func<ConformingContainer, T> factory)
    {
      _container.RegisterType<T>(new InjectionFactory(container => factory(_conformingContainer)));
    }

    public void Use(T instance)
    {
      _container.RegisterInstance(instance);
    }
  }

}