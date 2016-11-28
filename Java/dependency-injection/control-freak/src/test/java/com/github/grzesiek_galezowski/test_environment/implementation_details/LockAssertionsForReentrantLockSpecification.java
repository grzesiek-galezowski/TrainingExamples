package com.github.grzesiek_galezowski.test_environment.implementation_details;

import com.github.grzesiek_galezowski.test_environment.implementation_details.fixtures.LockAssertionsFixture;
import org.testng.annotations.Test;

import java.util.concurrent.locks.ReentrantLock;

/**
 * Created by astral on 20.03.2016.
 */
public class LockAssertionsForReentrantLockSpecification {


  @Test
  public void shouldThrowWhenAssertingThatUnlockedReentrantLockIsLocked() {
    //GIVEN
    final LockAssertionsFixture<ReentrantLock, LockAssertionsForReentrantLock>
        lockAssertionsFixture = LockAssertionsFixture.createReentrantLockFixture();
    //WHEN - THEN
    lockAssertionsFixture.assertThatLockedAssertionFails();

  }

  @Test
  public void shouldNotThrowWhenAssertingThatLockedReentrantLockIsLocked() {
    //GIVEN
    final LockAssertionsFixture<ReentrantLock, LockAssertionsForReentrantLock>
        lockAssertionsFixture = LockAssertionsFixture.createReentrantLockFixture();

    //WHEN - THEN
    lockAssertionsFixture.getLock().lock();
    lockAssertionsFixture.assertThatLockedAssertionPasses();
    lockAssertionsFixture.getLock().unlock();
  }

  @Test
  public void shouldThrowWhenAssertingThatLockedReentrantLockIsUnlocked() {
    //GIVEN
    final LockAssertionsFixture<ReentrantLock, LockAssertionsForReentrantLock>
        lockAssertionsFixture = LockAssertionsFixture.createReentrantLockFixture();

    //WHEN - THEN
    lockAssertionsFixture.getLock().lock();
    lockAssertionsFixture.assertThatUnlockedAssertionFails();
    lockAssertionsFixture.getLock().unlock();
  }

  @Test
  public void shouldNotThrowWhenAssertingThatUnlockedReentrantLockIsUnlocked() {
    //GIVEN
    final LockAssertionsFixture<ReentrantLock, LockAssertionsForReentrantLock>
        lockAssertionsFixture = LockAssertionsFixture.createReentrantLockFixture();

    //WHEN - THEN
    lockAssertionsFixture.assertThatUnlockedAssertionPasses();
  }

}