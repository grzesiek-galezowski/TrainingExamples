using Observer.Common;

namespace Observer._02_RemovingDetach;

internal class ExampleComposition
{
  public static void MainWannabe()
  {
    var observer1 = new ConcreteObserver();
    var observer2 = new ConcreteObserver();
    var observer3 = new ConcreteObserver();
    
    var cleanupJob = new CleanupJob(
      new WhateverCleanedUpDir(),
      new WhateverCleanedUpProcedure(),
      new WhateverSupport());

    cleanupJob.Attach(observer1);
    cleanupJob.Attach(observer2);
    cleanupJob.Attach(observer3);
  }
}