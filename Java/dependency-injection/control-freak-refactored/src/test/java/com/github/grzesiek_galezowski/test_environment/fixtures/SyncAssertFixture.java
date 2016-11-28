package com.github.grzesiek_galezowski.test_environment.fixtures;

import autofixture.publicinterface.Any;

import static org.mockito.Mockito.mock;

public class SyncAssertFixture {
  private InterfaceToBeSynchronized mock;
  private InterfaceToBeSynchronized realThing;
  private Integer a;
  private Integer b;

  public SyncAssertFixture() {
    this.mock = mock(InterfaceToBeSynchronized.class);
    this.realThing = new SynchronizedWrapperOverInterfaceToBeSynchronized(mock);
    this.a = Any.intValue();
    this.b = Any.intValue();
  }

  public static SyncAssertFixture create() {
    return new SyncAssertFixture();
  }

  public InterfaceToBeSynchronized getMock() {
    return mock;
  }

  public InterfaceToBeSynchronized getRealThing() {
    return realThing;
  }

  public Integer getA() {
    return a;
  }

  public Integer getB() {
    return b;
  }

}
