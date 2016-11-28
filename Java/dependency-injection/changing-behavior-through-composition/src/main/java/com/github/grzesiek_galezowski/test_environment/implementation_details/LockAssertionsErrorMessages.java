package com.github.grzesiek_galezowski.test_environment.implementation_details;

/**
 * Created by astral on 22.03.2016.
 */
public class LockAssertionsErrorMessages {
  static String lockHeldWhileExpectedNotHeld(final Object obj) {
    return "Expected this thread to not hold a lock on " + obj + " during a call to wrapped method, but it did";
  }

  static String lockNotHeldWhileExpectedHeld(final Object obj) {
    return "Expected this thread to hold a lock on " + obj + " during a call to wrapped method, but it didn't";
  }
}
