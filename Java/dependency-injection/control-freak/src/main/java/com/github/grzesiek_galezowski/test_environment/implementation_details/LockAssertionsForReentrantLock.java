package com.github.grzesiek_galezowski.test_environment.implementation_details;

import java.util.concurrent.locks.ReentrantLock;

import static org.assertj.core.api.AssertionsForInterfaceTypes.assertThat;

/**
 * Created by astral on 21.03.2016.
 */
public class LockAssertionsForReentrantLock implements LockAssertions {
  private final ReentrantLock lock;

  public LockAssertionsForReentrantLock(final ReentrantLock lock) {
    this.lock = lock;
  }

  @Override
  public void assertUnlocked() {
    assertThat(lock.isLocked()).withFailMessage(
        LockAssertionsErrorMessages.lockHeldWhileExpectedNotHeld(lock)).isFalse();
  }

  @Override
  public void assertLocked() {
    assertThat(lock.isLocked()).withFailMessage(
        LockAssertionsErrorMessages.lockNotHeldWhileExpectedHeld(lock)).isTrue();
  }
}
