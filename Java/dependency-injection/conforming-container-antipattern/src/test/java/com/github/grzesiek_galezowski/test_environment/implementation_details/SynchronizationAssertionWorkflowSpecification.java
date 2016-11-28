package com.github.grzesiek_galezowski.test_environment.implementation_details;

import com.github.grzesiek_galezowski.test_environment.fixtures.InterfaceToBeSynchronized;
import org.mockito.InOrder;
import org.testng.annotations.Test;

import static org.mockito.Mockito.doThrow;
import static org.mockito.Mockito.inOrder;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verifyZeroInteractions;
import static org.testng.Assert.assertTrue;

/**
 * Created by astral on 20.03.2016.
 */
public class SynchronizationAssertionWorkflowSpecification {
  @Test
  public void shouldPerformAllStepsInCorrectOrder() {
    //GIVEN
    final InterfaceToBeSynchronized interfaceMock = mock(InterfaceToBeSynchronized.class);
    final InterfaceToBeSynchronized proxy = mock(InterfaceToBeSynchronized.class);
    final SynchronizationAssertionSteps<InterfaceToBeSynchronized> steps
        = mock(SynchronizationAssertionSteps.class);
    final LockAssertions assertions = mock(LockAssertions.class);

    final SynchronizationAssertionWorkflow<InterfaceToBeSynchronized> workflow
        = new SynchronizationAssertionWorkflow<>(interfaceMock, proxy, steps);

    interfaceMock.correctlyCalledButNotSynchronizedFunction(1, 2);

    //WHEN
    workflow.invoke(assertions);

    //THEN
    final InOrder inOrder = inOrder(steps, assertions);
    inOrder.verify(assertions).assertUnlocked();
    inOrder.verify(steps).prepareMockForCall(interfaceMock, proxy, assertions);
    inOrder.verify(steps).callMethodOnProxy(proxy);
    inOrder.verify(steps).assertMethodResult(interfaceMock);
    inOrder.verify(assertions).assertUnlocked();

    verifyZeroInteractions(interfaceMock);
  }

  @Test
  public void shouldResetTheMockAfterwards() {
    //GIVEN
    final InterfaceToBeSynchronized interfaceMock = mock(InterfaceToBeSynchronized.class);
    final InterfaceToBeSynchronized proxy = mock(InterfaceToBeSynchronized.class);
    final SynchronizationAssertionSteps<InterfaceToBeSynchronized> steps
        = mock(SynchronizationAssertionSteps.class);
    final LockAssertions assertions = mock(LockAssertions.class);

    final SynchronizationAssertionWorkflow<InterfaceToBeSynchronized> workflow
        = new SynchronizationAssertionWorkflow<>(interfaceMock, proxy, steps);


    //WHEN
    interactInAnyWayWith(interfaceMock);
    workflow.invoke(assertions);

    //THEN
    verifyZeroInteractions(interfaceMock);
  }

  @Test
  public void shouldResetTheMockAfterwardsInCaseOfException() {
    //GIVEN
    final InterfaceToBeSynchronized interfaceMock = mock(InterfaceToBeSynchronized.class);
    final InterfaceToBeSynchronized proxy = mock(InterfaceToBeSynchronized.class);
    final SynchronizationAssertionSteps<InterfaceToBeSynchronized> steps
        = mock(SynchronizationAssertionSteps.class);
    final LockAssertions assertions = mock(LockAssertions.class);

    final SynchronizationAssertionWorkflow<InterfaceToBeSynchronized> workflow
        = new SynchronizationAssertionWorkflow<>(interfaceMock, proxy, steps);

    doThrow(new RuntimeException()).when(steps).callMethodOnProxy(proxy);

    //WHEN
    interactInAnyWayWith(interfaceMock);

    try {
      workflow.invoke(assertions);
      assertTrue(false);
    } catch (final Throwable t) {
      assertTrue(true);
    }

    //THEN
    verifyZeroInteractions(interfaceMock);
  }


  private int interactInAnyWayWith(final InterfaceToBeSynchronized interfaceMock) {
    return interfaceMock.correctlyCalledButNotSynchronizedFunction(1, 2);
  }


}