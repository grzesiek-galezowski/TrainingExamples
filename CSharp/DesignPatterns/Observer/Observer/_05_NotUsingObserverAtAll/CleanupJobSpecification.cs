using NSubstitute;
using Observer.Common;

namespace Observer._05_NotUsingObserverAtAll;

public class CleanupJobSpecification
{
  [Test]
  public void ShouldRunCleanupProcedureOnFilesAndThenNotifyAllAttachedObserversInOrder()
  {
    //GIVEN
    var cleanedUpDir = Substitute.For<ICleanedUpDir>();
    var cleanUpProcedure = Substitute.For<ICleanUpProcedure>();
    var files = Any.ReadOnlyList<ICleanedUpFile>();
    var diagnosticSubsystem = Substitute.For<IDiagnosticSubsystem>();
    var cleanupJob = new CleanupJob(cleanedUpDir, cleanUpProcedure, diagnosticSubsystem);

    cleanedUpDir.GetFilesToCleanup().Returns(files);

    //WHEN
    cleanupJob.Run();

    //THEN
    Received.InOrder(() =>
    {
      cleanUpProcedure.RunOn(files);
      diagnosticSubsystem.HandleFilesCleanupSuccessful(files.Count);
    });
  }
}