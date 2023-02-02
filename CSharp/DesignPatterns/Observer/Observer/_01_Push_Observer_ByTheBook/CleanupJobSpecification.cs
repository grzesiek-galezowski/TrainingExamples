using NSubstitute;
using Observer.Common;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Collections;
using TddXt.XNSubstitute;
using static TddXt.AnyRoot.Root;

namespace Observer._01_Push_Observer_ByTheBook;

public class CleanupJobSpecification
{
  [Test]
  public void ShouldRunCleanupProcedureOnFilesWhenNoObserversAttached()
  {
    //GIVEN
    var cleanedUpDir = Substitute.For<ICleanedUpDir>();
    var cleanUpProcedure = Substitute.For<ICleanUpProcedure>();
    var files = Any.ReadOnlyList<ICleanedUpFile>();
    var cleanupJob = new CleanupJob(cleanedUpDir, cleanUpProcedure, Any.Instance<ISupport>());

    cleanedUpDir.GetFilesToCleanup().Returns(files);

    //WHEN
    cleanupJob.Run();

    //THEN
    cleanUpProcedure.Received(1).RunOn(files);
  }
  
  [Test]
  public void ShouldRunCleanupProcedureOnFilesAndThenNotifyAllAttachedObserversInOrder()
  {
    //GIVEN
    var cleanedUpDir = Substitute.For<ICleanedUpDir>();
    var cleanUpProcedure = Substitute.For<ICleanUpProcedure>();
    var files = Any.ReadOnlyList<ICleanedUpFile>();
    var cleanupJob = new CleanupJob(cleanedUpDir, cleanUpProcedure, Any.Instance<ISupport>());
    var observer1 = Substitute.For<ICleanupObserver>();
    var observer2 = Substitute.For<ICleanupObserver>();
    var observer3 = Substitute.For<ICleanupObserver>();

    cleanupJob.Attach(observer1);
    cleanupJob.Attach(observer2);
    cleanupJob.Attach(observer3);
    
    cleanedUpDir.GetFilesToCleanup().Returns(files);

    //WHEN
    cleanupJob.Run();

    //THEN
    Received.InOrder(() =>
    {
      cleanUpProcedure.RunOn(files);
      observer1.OnCleanupSuccessful(files.Count);
      observer2.OnCleanupSuccessful(files.Count);
      observer3.OnCleanupSuccessful(files.Count);
    });
  }
  
  [Test]
  public void ShouldNotNotifyDetachedObserversAfterCleanupProcedure()
  {
    //GIVEN
    var cleanedUpDir = Substitute.For<ICleanedUpDir>();
    var cleanUpProcedure = Substitute.For<ICleanUpProcedure>();
    var files = Any.ReadOnlyList<ICleanedUpFile>();
    var cleanupJob = new CleanupJob(cleanedUpDir, cleanUpProcedure, Any.Instance<ISupport>());
    var observer1 = Substitute.For<ICleanupObserver>();
    var observer2 = Substitute.For<ICleanupObserver>();
    var observer3 = Substitute.For<ICleanupObserver>();

    cleanupJob.Attach(observer1);
    cleanupJob.Attach(observer2);
    cleanupJob.Attach(observer3);
    cleanupJob.Detach(observer2);
    
    cleanedUpDir.GetFilesToCleanup().Returns(files);

    //WHEN
    cleanupJob.Run();

    //THEN
    Received.InOrder(() =>
    {
      cleanUpProcedure.RunOn(files);
      observer1.OnCleanupSuccessful(files.Count);
      observer3.OnCleanupSuccessful(files.Count);
    });
    observer2.ReceivedNothing();
  }

  [Test]
  public void ShouldNotifyAllAttachedObserversAfterCleanupProcedureAndLogAnyExceptionsFromThem()
  {
    //GIVEN
    var cleanedUpDir = Substitute.For<ICleanedUpDir>();
    var cleanUpProcedure = Substitute.For<ICleanUpProcedure>();
    var files = Any.ReadOnlyList<ICleanedUpFile>();
    var support = Substitute.For<ISupport>();
    var exception = Any.Exception();
    var cleanupJob = new CleanupJob(cleanedUpDir, cleanUpProcedure, support);
    var observer1 = Substitute.For<ICleanupObserver>();
    var observer2 = Substitute.For<ICleanupObserver>();
    var observer3 = Substitute.For<ICleanupObserver>();

    cleanupJob.Attach(observer1);
    cleanupJob.Attach(observer2);
    cleanupJob.Attach(observer3);

    cleanedUpDir.GetFilesToCleanup().Returns(files);
    observer2
      .When(s => s.OnCleanupSuccessful(files.Count))
      .Throw(exception);

    //WHEN
    cleanupJob.Run();

    //THEN
    Received.InOrder(() =>
    {
      cleanUpProcedure.RunOn(files);
      observer1.OnCleanupSuccessful(files.Count);
      observer2.OnCleanupSuccessful(files.Count);
      support.NotifyingObserverFailed(exception, observer2.GetType(), files.Count);
      observer3.OnCleanupSuccessful(files.Count);
    });
  }

}