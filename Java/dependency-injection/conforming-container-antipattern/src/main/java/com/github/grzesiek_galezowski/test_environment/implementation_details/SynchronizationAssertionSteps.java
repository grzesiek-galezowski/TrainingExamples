package com.github.grzesiek_galezowski.test_environment.implementation_details;

/**
 * Created by astral on 18.03.2016.
 */
public interface SynchronizationAssertionSteps<T> {
  void assertMethodResult(T wrappedInterfaceMock);

  void prepareMockForCall(T wrappedInterfaceMock, T synchronizedProxy, LockAssertions lockAssertions);

  void callMethodOnProxy(T synchronizedProxy);
}
