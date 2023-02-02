using Observer.Common;

namespace Observer._01_PushObserverByTheBook;

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
    cleanupJob.Detach(observer3);
  }
}