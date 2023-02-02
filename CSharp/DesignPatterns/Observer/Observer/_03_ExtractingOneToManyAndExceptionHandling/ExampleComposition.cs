using Observer.Common;

namespace Observer._03_ExtractingOneToManyAndExceptionHandling;

internal class ExampleComposition
{
  public static void MainWannabe1()
  {
    var observer = new ConcreteObserver();
    
    var cleanupJob = new CleanupJob(
      new WhateverCleanedUpDir(),
      new WhateverCleanedUpProcedure());

    cleanupJob.Set(observer);
  }
  
  public static void MainWannabe2()
  {
    var observer1 = new ConcreteObserver();
    var observer2 = new ConcreteObserver();
    var observer3 = new ConcreteObserver();

    var cleanupJob = new CleanupJob(
      new WhateverCleanedUpDir(),
      new WhateverCleanedUpProcedure());

    cleanupJob.Set(BroadcastingObserverWith(observer1, observer2, observer3));
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
}