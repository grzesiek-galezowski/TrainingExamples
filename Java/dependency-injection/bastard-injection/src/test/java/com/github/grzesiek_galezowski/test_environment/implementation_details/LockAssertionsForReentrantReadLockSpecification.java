package com.github.grzesiek_galezowski.test_environment.implementation_details;

import com.github.grzesiek_galezowski.test_environment.implementation_details.fixtures.LockAssertionsFixture;
import org.testng.annotations.Test;

import java.util.concurrent.locks.ReentrantReadWriteLock;

/**
 * Created by astral on 20.03.2016.
 */
public class LockAssertionsForReentrantReadLockSpecification {

  @Test
  public void shouldThrowWhenAssertingThatUnlockedReentrantLockIsLocked() {
    //GIVEN
    final LockAssertionsFixture<ReentrantReadWriteLock, LockAssertionsForReentrantReadLock>
        fixture = LockAssertionsFixture.createReentrantReadLockFixture();

    //WHEN - THEN
    fixture.assertThatLockedAssertionFails();

  }

  @Test
  public void shouldNotThrowWhenAssertingThatLockedReentrantLockIsLocked() {
    //GIVEN
    final LockAssertionsFixture<ReentrantReadWriteLock, LockAssertionsForReentrantReadLock>
        fixture = LockAssertionsFixture.createReentrantReadLockFixture();

    //WHEN - THEN
    fixture.getLock().readLock().lock();
    fixture.assertThatLockedAssertionPasses();
    fixture.getLock().readLock().unlock();
  }

  @Test
  public void shouldThrowWhenAssertingThatLockedReentrantLockIsUnlocked() {
    //GIVEN
    final LockAssertionsFixture<ReentrantReadWriteLock, LockAssertionsForReentrantReadLock>
        fixture = LockAssertionsFixture.createReentrantReadLockFixture();

    //WHEN - THEN
    fixture.getLock().readLock().lock();
    fixture.assertThatUnlockedAssertionFails();
    fixture.getLock().readLock().unlock();
  }

  @Test
  public void shouldNotThrowWhenAssertingThatUnlockedReentrantLockIsUnlocked() {
    //GIVEN
    final LockAssertionsFixture<ReentrantReadWriteLock, LockAssertionsForReentrantReadLock>
        fixture = LockAssertionsFixture.createReentrantReadLockFixture();

    //WHEN - THEN
    fixture.assertThatUnlockedAssertionPasses();
  }


}