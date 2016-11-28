package com.github.grzesiek_galezowski.test_environment.implementation_details;

import org.mockito.stubbing.Answer;

import javax.annotation.Nullable;
import java.util.function.Function;

import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.when;

/**
 * Created by astral whenReceives 07.03.2016.
 */
public class StepsForSynchronizingOnFunction<T, TReturn>
    implements SynchronizationAssertionSteps<T> {
  private final TReturn retVal;

  @Nullable
  private TReturn resultFromWrapper;
  private final Function<T, TReturn> methodCallToVerify;

  public StepsForSynchronizingOnFunction(
      final Function<T, TReturn> methodCallToVerify,
      final TReturn retVal) {
    this.methodCallToVerify = methodCallToVerify;
    this.retVal = retVal;
  }

  @Override
  public void assertMethodResult(final T wrappedInterfaceMock) {
    assertThat(resultFromWrapper).isEqualTo(retVal);
  }

  @Override
  public void prepareMockForCall(final T wrappedInterfaceMock, final T synchronizedProxy, final LockAssertions lockAssertions) {
    when(methodCallToVerify.apply(wrappedInterfaceMock))
        .then((Answer<TReturn>) invocation -> {
          lockAssertions.assertLocked();
          return retVal;
        });
  }

  @Override
  public void callMethodOnProxy(final T synchronizedProxy) {
    resultFromWrapper = methodCallToVerify.apply(synchronizedProxy);
  }
}
