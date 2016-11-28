package com.github.grzesiek_galezowski.test_environment.implementation_details;

import static org.mockito.Mockito.reset;

public class SynchronizationAssertionWorkflow<T> {
  private final T wrappedInterfaceMock;
  private final T synchronizedProxy;
  private final SynchronizationAssertionSteps<T> steps;

  public SynchronizationAssertionWorkflow(
      final T wrappedInterfaceMock,
      final T synchronizedProxy,
      final SynchronizationAssertionSteps<T> steps) {

    this.wrappedInterfaceMock = wrappedInterfaceMock;
    this.synchronizedProxy = synchronizedProxy;
    this.steps = steps;
  }

  public void invoke(final LockAssertions lockAssertions) {
    try {
      lockAssertions.assertUnlocked();
      steps.prepareMockForCall(wrappedInterfaceMock, synchronizedProxy, lockAssertions);

      //WHEN
      steps.callMethodOnProxy(synchronizedProxy);

      //THEN
      steps.assertMethodResult(wrappedInterfaceMock);
      lockAssertions.assertUnlocked();
    } finally {
      reset(wrappedInterfaceMock);
    }
  }

}
