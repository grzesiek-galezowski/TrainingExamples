using System;
using NSubstitute;
using NUnit.Framework;
using ServiceLocatorDIAntipattern._2_ServiceLocator;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Core;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Services;
using Unity;
using Unity.Lifetime;

namespace ServiceLocatorDIAntipattern
{
    public class Class1
    {
      [Test]
      public void ShouldStartProperly___DUMMY___()
      {
        var mockRepository = Substitute.For<IRepository>();
        ApplicationRoot.Context.RegisterInstance(mockRepository);

        var telecom = new TeleComSystem();

        //fails on purpose - lost dependency
        telecom.Start();
      }

      [Test]
      public void ShouldDisposeOfDisposableDependencies()
      {
        //GIVEN
        Assume.That(Disposable._disposed, Is.EqualTo(false));

        //WHEN
        using (var container = new UnityContainer())
        {
          container.RegisterType<IEmpty, Disposable>(new ContainerControlledLifetimeManager());

          container.Resolve<IEmpty>();
        } // release

        //THEN
        Assert.AreEqual(true, Disposable._disposed);
      }

      public class Disposable : IDisposable, IEmpty
      {
        public static bool _disposed = false;

        public void Dispose()
        {
          _disposed = true;
        }
      }
    }

  public interface IEmpty
  {
  }
}
