package com.github.grzesiek_galezowski.test_environment.implementation_details;

import org.assertj.core.api.Assertions;

public class LockAssertionsForMonitor implements LockAssertions {
  private final Object monitorObject;

  public LockAssertionsForMonitor(final Object monitorObject) {
    this.monitorObject = monitorObject;
  }

  @Override
  public void assertUnlocked() {
    assertLockNotHeldOn(monitorObject);
  }

  @Override
  public void assertLocked() {
    assertThreadHoldsALockOn(monitorObject);
  }

  private static void assertLockNotHeldOn(final Object monitor) {
    Assertions.assertThat(Thread.holdsLock(monitor)).withFailMessage(
        LockAssertionsErrorMessages.lockHeldWhileExpectedNotHeld(monitor)).isFalse();
  }

  private void assertThreadHoldsALockOn(final Object monitor) {
    Assertions.assertThat(Thread.holdsLock(monitor)).withFailMessage(
        LockAssertionsErrorMessages.lockNotHeldWhileExpectedHeld(monitor)).isTrue();
  }

}