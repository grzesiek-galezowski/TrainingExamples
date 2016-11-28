package com.github.grzesiek_galezowski.test_environment.implementation_details;

import java.util.concurrent.locks.ReentrantReadWriteLock;

import static org.assertj.core.api.AssertionsForClassTypes.assertThat;

/**
 * Created by astral on 21.03.2016.
 */
public class LockAssertionsForReentrantWriteLock implements LockAssertions {
  private final ReentrantReadWriteLock lock;

  public LockAssertionsForReentrantWriteLock(final ReentrantReadWriteLock lock) {
    this.lock = lock;
  }

  @Override
  public void assertUnlocked() {
    assertThat(lock.isWriteLocked()).withFailMessage(
        LockAssertionsErrorMessages.lockHeldWhileExpectedNotHeld(lock)).isFalse();
  }

  @Override
  public void assertLocked() {
    assertThat(lock.isWriteLocked()).withFailMessage(
        LockAssertionsErrorMessages.lockNotHeldWhileExpectedHeld(lock)).isTrue();
  }
}
