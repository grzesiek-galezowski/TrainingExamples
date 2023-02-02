using NSubstitute;
using Observer.Common;

namespace Observer._03_ExtractingOneToManyAndExceptionHandling;

public class CleanupJobSpecification
{
  [Test]
  public void ShouldRunCleanupProcedureOnFilesWhenNoObserversAttached()
  {
    //GIVEN
    var cleanedUpDir = Substitute.For<ICleanedUpDir>();
    var cleanUpProcedure = Substitute.For<ICleanUpProcedure>();
    var files = Any.ReadOnlyList<ICleanedUpFile>();
    var cleanupJob = new CleanupJob(cleanedUpDir, cleanUpProcedure);

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
    var cleanupJob = new CleanupJob(cleanedUpDir, cleanUpProcedure);
    var observer = Substitute.For<ICleanupObserver>();

    cleanupJob.Set(observer);
    
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