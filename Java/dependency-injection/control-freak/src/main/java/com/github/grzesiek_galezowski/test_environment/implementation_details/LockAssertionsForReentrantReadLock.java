package com.github.grzesiek_galezowski.test_environment.implementation_details;

import java.util.concurrent.locks.ReentrantReadWriteLock;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;

/**
 * Created by astral on 21.03.2016.
 */
public class LockAssertionsForReentrantReadLock implements LockAssertions {
  private final ReentrantReadWriteLock lock;

  public LockAssertionsForReentrantReadLock(final ReentrantReadWriteLock lock) {
    this.lock = lock;
  }

  @Override
  public void assertUnlocked() {
    assertThat(lock.getReadHoldCount() == 0).withFailMessage(
        LockAssertionsErrorMessages.lockHeldWhileExpectedNotHeld(lock)).isTrue();
  }

  @Override
  public void assertLocked() {
    assertThat(lock.getReadHoldCount() != 0).withFailMessage(
        LockAssertionsErrorMessages.lockNotHeldWhileExpectedHeld(lock)).isTrue();
  }
}
