using NSubstitute;
using Observer.Common;

namespace Observer._04_UsingConstructorInjection;

public class CleanupJobSpecification
{
  [Test]
  public void ShouldRunCleanupProcedureOnFilesAndThenNotifyAllAttachedObserversInOrder()
  {
    //GIVEN
    var cleanedUpDir = Substitute.For<ICleanedUpDir>();
    var cleanUpProcedure = Substitute.For<ICleanUpProcedure>();
    var files = Any.ReadOnlyList<ICleanedUpFile>();
    var observer = Substitute.For<ICleanupObserver>();
    var cleanupJob = new CleanupJob(cleanedUpDir, cleanUpProcedure, observer);

    cleanedUpDir.GetFilesToCleanup().Returns(files);

    //WHEN
    cleanupJob.Run();

    //THEN
    Received.InOrder(() =>
    {
      cleanUpProcedure.RunOn(files);
      observer.OnCleanupSuccessful(files.Count);
    });
  }
}