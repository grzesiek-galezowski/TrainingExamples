package com.github.grzesiek_galezowski.test_environment.implementation_details;

import org.mockito.Mockito;
import org.mockito.stubbing.Answer;

import java.util.function.Consumer;

import static org.mockito.Mockito.verify;

/**
 * Created by astral whenReceives 07.03.2016.
 */
public class StepsForSynchronizingOnCommand<T>
    implements SynchronizationAssertionSteps<T> {
  private final Consumer<T> methodCallToVerify;

  public StepsForSynchronizingOnCommand(
      final Consumer<T> methodCallToVerify) {
    this.methodCallToVerify = methodCallToVerify;
  }

  @Override
  public void assertMethodResult(final T wrappedInterfaceMock) {
    methodCallToVerify.accept(verify(wrappedInterfaceMock));
  }

  @Override
  public void callMethodOnProxy(final T synchronizedProxy) {
    methodCallToVerify.accept(synchronizedProxy);
  }

  @Override
  public void prepareMockForCall(final T wrappedInterfaceMock,
                                 final T synchronizedProxy,
                                 final LockAssertions lockAssertions) {
    methodCallToVerify.accept(
        Mockito.doAnswer((Answer<Void>) invocation -> {
          lockAssertions.assertLocked();
          return null;
        }).when(wrappedInterfaceMock)
    );
  }

}
