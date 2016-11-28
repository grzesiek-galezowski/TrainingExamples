package com.github.grzesiek_galezowski.test_environment.implementation_details;

import com.github.grzesiek_galezowski.test_environment.implementation_details.fixtures.LockAssertionsFixture;
import org.testng.annotations.Test;


public class LockAssertionsForMonitorSpecification {
  @Test
  public void shouldThrowWhenAssertingThatUnlockedMonitorIsLocked() {
    //GIVEN
    final LockAssertionsFixture<Object, LockAssertionsForMonitor> fixture =
        LockAssertionsFixture.createMonitorFixture();
    //WHEN - THEN
    fixture.assertThatLockedAssertionFails();

  }

  @Test
  public void shouldNotThrowWhenAssertingThatLockedMonitorIsLocked() {
    //GIVEN
    final LockAssertionsFixture<Object, LockAssertionsForMonitor> fixture =
        LockAssertionsFixture.createMonitorFixture();
    //WHEN - THEN
    synchronized (fixture.getLock()) {
      fixture.assertThatLockedAssertionPasses();
    }
  }

  @Test
  public void shouldThrowWhenAssertingThatLockedMonitorIsUnlocked() {
    //GIVEN
    final LockAssertionsFixture<Object, LockAssertionsForMonitor> fixture =
        LockAssertionsFixture.createMonitorFixture();

    //WHEN - THEN
    synchronized (fixture.getLock()) {
      fixture.assertThatUnlockedAssertionFails();
    }

  }

  @Test
  public void shouldNotThrowWhenAssertingThatUnlockedMonitorIsUnlocked() {
    //GIVEN
    final LockAssertionsFixture<Object, LockAssertionsForMonitor> fixture =
        LockAssertionsFixture.createMonitorFixture();

    //WHEN - THEN
    fixture.assertThatUnlockedAssertionPasses();
  }


}