package com.github.grzesiek_galezowski.test_environment;

import com.github.grzesiek_galezowski.test_environment.fixtures.InterfaceToBeSynchronized;
import com.github.grzesiek_galezowski.test_environment.fixtures.SyncAssertFixture;
import org.testng.annotations.Test;

import java.util.function.Consumer;

import static com.github.grzesiek_galezowski.test_environment.XAssert.assertThatProxyTo;
import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

public class LockingVoidMethodAssertionsSpecification {

  @Test
  public void shouldPassWhenVoidMethodIsCalledCorrectlyInSynchronizedBlock() {
    //GIVEN
    final SyncAssertFixture syncAssertFixture = SyncAssertFixture.create();
    //WHEN-THEN

    assertThatProxyTo(syncAssertFixture.getMock(), syncAssertFixture.getRealThing())
        .whenReceives(instance -> instance.correctlyWrappedVoidMethod(syncAssertFixture.getA(), syncAssertFixture.getB()))
        .thenLocksCorrectly();
    assertThat(Thread.holdsLock(syncAssertFixture.getRealThing())).isFalse();
  }

  @Test
  public void shouldFailWhenVoidMethodIsCalledCorrectlyButNotInSynchronizedBlock() {
    //GIVEN
    final SyncAssertFixture syncAssertFixture = SyncAssertFixture.create();

    //WHEN-THEN
    assertExceptionIsThrownOn(
        instance -> instance.correctlyCalledButNotSynchronizedVoidMethod(syncAssertFixture.getA(), syncAssertFixture.getB()));
  }

  @Test
  public void shouldFailWhenVoidMethodIsNotCalledAtAll() {
    //GIVEN
    final SyncAssertFixture syncAssertFixture = SyncAssertFixture.create();

    //WHEN-THEN
    assertExceptionIsThrownOn(instance -> instance.voidMethodNotCalledAtAll(syncAssertFixture.getA(), syncAssertFixture.getB()));
  }

  @Test
  public void shouldFailWhenVoidMethodIsSynchronizedButCalledWithWrongArguments() {
    //GIVEN
    final SyncAssertFixture syncAssertFixture = SyncAssertFixture.create();

    //WHEN-THEN
    assertExceptionIsThrownOn(instance -> {
      instance.voidMethodCalledWithWrongArguments(syncAssertFixture.getA(), syncAssertFixture.getB());
    });
  }

  private void assertExceptionIsThrownOn(final Consumer<InterfaceToBeSynchronized> consumer) {
    //GIVEN
    final SyncAssertFixture syncAssertFixture = SyncAssertFixture.create();

    assertThatThrownBy(() -> assertThatProxyTo(
        syncAssertFixture.getMock(), syncAssertFixture.getRealThing()).whenReceives(consumer).thenLocksCorrectly()
    ).isInstanceOf(AssertionError.class);
    assertThat(Thread.holdsLock(syncAssertFixture.getRealThing())).isFalse();
  }


  //TODO tests for:
  //4. release whenReceives throwing exception
}

