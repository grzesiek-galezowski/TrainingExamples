package com.github.grzesiek_galezowski.test_environment.implementation_details;

import java.util.concurrent.locks.ReentrantLock;
import java.util.concurrent.locks.ReentrantReadWriteLock;

/**
 * Created by astral whenReceives 17.03.2016.
 */
public class SynchronizationAssertDsl2<T> {
  private final T realThing;
  private final SynchronizationAssertionWorkflow<T> assertionWorkflow;

  public SynchronizationAssertDsl2(final T realThing,
                                   final SynchronizationAssertionWorkflow<T> assertionWorkflow) {

    this.realThing = realThing;
    this.assertionWorkflow = assertionWorkflow;
  }

  public void thenLocksCorrectly() {
    thenLocksCorrectlyOn(realThing);
  }

  public void thenLocksCorrectlyOn(final Object monitorObject) {
    final LockAssertionsForMonitor lockAssertions = new LockAssertionsForMonitor(monitorObject);
    assertionWorkflow.invoke(lockAssertions);
  }

  public void thenLocksCorrectlyOn(final ReentrantLock lock) {
    final LockAssertionsForReentrantLock lockAssertions = new LockAssertionsForReentrantLock(lock);
    assertionWorkflow.invoke(lockAssertions);
  }

  public void thenLocksReadCorrectlyOn(final ReentrantReadWriteLock lock) {
    final LockAssertionsForReentrantReadLock lockAssertions = new LockAssertionsForReentrantReadLock(lock);
    assertionWorkflow.invoke(lockAssertions);
  }

  public void thenLocksWriteCorrectlyOn(final ReentrantReadWriteLock lock) {
    final LockAssertionsForReentrantWriteLock lockAssertions = new LockAssertionsForReentrantWriteLock(lock);
    assertionWorkflow.invoke(lockAssertions);
  }

}
