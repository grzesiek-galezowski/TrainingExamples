package com.github.grzesiek_galezowski.test_environment;

import autofixture.publicinterface.InstanceOf;
import com.github.grzesiek_galezowski.test_environment.fixtures.InterfaceToBeSynchronized;
import com.github.grzesiek_galezowski.test_environment.fixtures.SyncAssertFixture;
import org.testng.annotations.Test;

import java.util.List;
import java.util.function.Function;

import static com.github.grzesiek_galezowski.test_environment.XAssert.assertThatProxyTo;
import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

public class LockingFunctionsWithGenericReturnTypeAssertionsSpecification {

  @Test
  public void shouldPassWhenFunctionIsCalledCorrectlyInSynchronizedBlock() {
    //GIVEN
    final SyncAssertFixture fixture = new SyncAssertFixture();

    //WHEN-THEN
    assertThatProxyTo(fixture.getMock(), fixture.getRealThing())
        .whenReceives(
            instance -> instance.genericCorrectlySynchronizedFunction(fixture.getA(), fixture.getB()),
            new InstanceOf<List<Integer>>() {
            })
        .thenLocksCorrectly();
    assertThat(Thread.holdsLock(fixture.getRealThing())).isFalse();
  }

  @Test
  public void shouldFailWhenVoidMethodIsCalledCorrectlyButNotInSynchronizedBlock() {
    final SyncAssertFixture fixture = new SyncAssertFixture();
    //WHEN-THEN
    assertThrowsWhen(instance ->
            instance.genericCorrectlyCalledButNotSynchronizedFunction(fixture.getA(), fixture.getB()),
        fixture);
  }

  @Test
  public void shouldFailWhenFunctionIsNotCalledAtAll() {
    //GIVEN
    final SyncAssertFixture fixture = new SyncAssertFixture();

    //WHEN-THEN
    assertThrowsWhen(instance -> instance.genericFunctionNotCalledAtAll(
        fixture.getA(), fixture.getB()), fixture);
  }

  @Test
  public void shouldFailWhenFunctionIsSynchronizedButCalledWithWrongArguments() {
    //GIVEN
    final SyncAssertFixture fixture = new SyncAssertFixture();

    //WHEN-THEN
    assertThrowsWhen(
        instance -> instance.genericFunctionCalledWithWrongArguments(
            fixture.getA(), fixture.getB()), fixture);
  }

  @Test
  public void shouldFailWhenFunctionIsSynchronizedButItsReturnValueIsNotPropagatedBack() {
    //GIVEN
    final SyncAssertFixture fixture = new SyncAssertFixture();

    //WHEN-THEN
    assertThrowsWhen(instance -> instance.genericFunctionWithNonPropagatedReturnValue(
        fixture.getA(), fixture.getB()), fixture);
  }

  private void assertThrowsWhen(final Function<InterfaceToBeSynchronized,
      List<Integer>> function, final SyncAssertFixture fixture) {
    assertThatThrownBy(() -> assertThatProxyTo(fixture.getMock(), fixture.getRealThing())
        .whenReceives(function, new InstanceOf<List<Integer>>() {
        }).thenLocksCorrectly()
    ).isInstanceOf(AssertionError.class);
    assertThat(Thread.holdsLock(fixture.getRealThing())).isFalse();
  }

  //TODO
  //4. release whenReceives throwing exception
}
