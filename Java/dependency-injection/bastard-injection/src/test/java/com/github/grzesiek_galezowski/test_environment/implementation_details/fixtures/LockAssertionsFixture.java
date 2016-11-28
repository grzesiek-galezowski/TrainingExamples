package com.github.grzesiek_galezowski.test_environment.implementation_details.fixtures;

import com.github.grzesiek_galezowski.test_environment.implementation_details.LockAssertions;
import com.github.grzesiek_galezowski.test_environment.implementation_details.LockAssertionsForMonitor;
import com.github.grzesiek_galezowski.test_environment.implementation_details.LockAssertionsForReentrantLock;
import com.github.grzesiek_galezowski.test_environment.implementation_details.LockAssertionsForReentrantReadLock;
import com.github.grzesiek_galezowski.test_environment.implementation_details.LockAssertionsForReentrantWriteLock;

import java.util.concurrent.locks.ReentrantLock;
import java.util.concurrent.locks.ReentrantReadWriteLock;

import static com.github.grzesiek_galezowski.test_environment.XAssert.assertThatNotThrownBy;
import static org.assertj.core.api.AssertionsForClassTypes.assertThatThrownBy;

public class LockAssertionsFixture<T, U extends LockAssertions> {
  private T lock;
  private U assertions;

  public static LockAssertionsFixture<ReentrantLock, LockAssertionsForReentrantLock>
  createReentrantLockFixture() {
    final ReentrantLock reentrantLock = new ReentrantLock();
    return new LockAssertionsFixture<>(
        reentrantLock, new LockAssertionsForReentrantLock(reentrantLock));
  }

  public static LockAssertionsFixture<ReentrantReadWriteLock, LockAssertionsForReentrantReadLock>
  createReentrantReadLockFixture() {
    final ReentrantReadWriteLock reentrantReadWriteLock = new ReentrantReadWriteLock();
    return new LockAssertionsFixture<>(
        reentrantReadWriteLock, new LockAssertionsForReentrantReadLock(reentrantReadWriteLock));
  }

  public static LockAssertionsFixture<ReentrantReadWriteLock, LockAssertionsForReentrantWriteLock>
  createReentrantWriteLockFixture() {
    final ReentrantReadWriteLock reentrantReadWriteLock = new ReentrantReadWriteLock();
    return new LockAssertionsFixture<>(
        reentrantReadWriteLock, new LockAssertionsForReentrantWriteLock(reentrantReadWriteLock));
  }

  public static LockAssertionsFixture<Object, LockAssertionsForMonitor>
  createMonitorFixture() {
    final Object o = new Object();
    return new LockAssertionsFixture<>(o, new LockAssertionsForMonitor(o));
  }

  public void assertThatLockedAssertionPasses() {
    assertThatNotThrownBy(() -> assertLocked());
  }

  public void assertThatUnlockedAssertionPasses() {
    assertThatNotThrownBy(() -> assertUnlocked());
  }


  public void assertThatUnlockedAssertionFails() {
    assertThatThrownBy(() -> assertUnlocked())
        .hasMessageStartingWith("Expected this thread to not hold a lock on ")
        .hasMessageEndingWith("during a call to wrapped method, but it did");
  }

  public void assertThatLockedAssertionFails() {
    assertThatThrownBy(() -> assertLocked())
        .hasMessageStartingWith("Expected this thread to hold a lock on ")
        .hasMessageEndingWith("during a call to wrapped method, but it didn't");
  }

  public void assertLocked() {
    assertions.assertLocked();
  }

  public void assertUnlocked() {
    assertions.assertUnlocked();
  }


  public T getLock() {
    return lock;
  }

  public LockAssertionsFixture(
      final T lock, final U assertions) {

    this.lock = lock;
    this.assertions = assertions;
  }
}

