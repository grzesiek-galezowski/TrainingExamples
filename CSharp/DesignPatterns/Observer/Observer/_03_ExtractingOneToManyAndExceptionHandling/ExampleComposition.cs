using Observer.Common;
// ReSharper disable UnusedMember.Global

namespace Observer._03_ExtractingOneToManyAndExceptionHandling;

internal class ExampleComposition
{
  public static void JustASingleObserver()
  {
    var observer = new Telemetry();
    
    var cleanupJob = new CleanupJob(
      new WhateverCleanedUpDir(),
      new WhateverCleanedUpProcedure());

    cleanupJob.Set(observer);
  }
  
  public static void ProperOneToManyObserver()
  {
    var observer1 = new Telemetry();
    var observer2 = new ThresholdAlertDetection();
    var observer3 = new CleanupHistory();

    var cleanupJob = new CleanupJob(
      new WhateverCleanedUpDir(),
      new WhateverCleanedUpProcedure());

    cleanupJob.Set(BroadcastingObserverWith(observer1, observer2, observer3));
  }
  
  public static void ParallelOneToManyObserver()
  {
    var observer1 = new Telemetry();
    var observer2 = new ThresholdAlertDetection();
    var observer3 = new CleanupHistory();

    var cleanupJob = new CleanupJob(
      new WhateverCleanedUpDir(),
      new WhateverCleanedUpProcedure());

    cleanupJob.Set(ParallelBroadcastingObserverWith(observer1, observer2, observer3));
  }

  /// <summary>
  /// If we have the method anyway, why not use the constructor?
  /// </summary>
  private static BroadcastingObserver BroadcastingObserverWith(
    ICleanupObserver observer1, 
    ICleanupObserver observer2,
    ICleanupObserver observer3)
  {
    var broadcastingObserver = new BroadcastingObserver(new WhateverSupport());
    broadcastingObserver.Attach(observer1);
    broadcastingObserver.Attach(observer2);
    broadcastingObserver.Attach(observer3);
    return broadcastingObserver;
  }
  
  /// <summary>
  /// If we have the method anyway, why not use the constructor?
  /// </summary>
  private static ParallelBroadcastingObserver ParallelBroadcastingObserverWith(
    ICleanupObserver observer1, 
    ICleanupObserver observer2,
    ICleanupObserver observer3)
  {
    var broadcastingObserver = new ParallelBroadcastingObserver(new WhateverSupport());
    broadcastingObserver.Attach(observer1);
    broadcastingObserver.Attach(observer2);
    broadcastingObserver.Attach(observer3);
    return broadcastingObserver;
  }
}