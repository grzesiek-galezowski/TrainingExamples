using System;
using ConformingContainerAntipattern._3_ConformingContainer;
using ConformingContainerAntipattern._3_ConformingContainer.Core;
using ConformingContainerAntipattern._3_ConformingContainer.Services;
using NSubstitute;
using NUnit.Framework;

namespace ConformingContainerAntipattern
{
  namespace ServiceLocatorDIAntipattern
  {
    public class Class1
    {
      [Test]
      public void ShouldStartProperly___DUMMY___()
      {
        var mockRepository = Substitute.For<IRepository>();
        ApplicationRoot.Context.For<IRepository>().Use(mockRepository);

        var telecom = new TeleComSystem();

      }

      [Test]
      public void ShouldDisposeOfDisposableDependencies()
      {
        //GIVEN
        Assume.That(Disposable._disposed, Is.EqualTo(false));

        //WHEN
        using (var container = new ConformingContainer())
        {
          container.For<IEmpty>().UseAlwaysTheSame<Disposable>();

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

}
