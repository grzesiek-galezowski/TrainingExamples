using Observer.Common;

// ReSharper disable UnusedMember.Global

namespace Observer._05_NotUsingObserverAtAll;

internal class ExampleComposition
{
  public static void JustSomeRoleModelingAndCodeComposition()
  {
    var cleanupJob = new CleanupJob(
      new WhateverCleanedUpDir(),
      new WhateverCleanedUpProcedure(),
      new DiagnosticSubsystem(
        new Telemetry(),
        new ThresholdAlertDetection(),
        new CleanupHistory()));
  }
}