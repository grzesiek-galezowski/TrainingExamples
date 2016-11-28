package com.github.grzesiek_galezowski.test_environment.fixtures;

import autofixture.publicinterface.Any;
import edu.umd.cs.findbugs.annotations.SuppressFBWarnings;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by astral whenReceives 06.03.2016.
 */
public class SynchronizedWrapperOverInterfaceToBeSynchronized implements InterfaceToBeSynchronized {

  private final InterfaceToBeSynchronized iface;

  public SynchronizedWrapperOverInterfaceToBeSynchronized(final InterfaceToBeSynchronized iface) {

    this.iface = iface;
  }

  @Override
  public void correctlyWrappedVoidMethod(final int a, final int b) {
    synchronized (this) {
      iface.correctlyWrappedVoidMethod(a, b);
    }
  }

  @Override
  public int correctlyWrappedFunction(final int a, final int b) {
    synchronized (this) {
      return iface.correctlyWrappedFunction(a, b);
    }
  }

  @Override
  public List<Integer> genericCorrectlySynchronizedFunction(final int a, final int b) {
    synchronized (this) {
      return iface.genericCorrectlySynchronizedFunction(a, b);
    }
  }

  @Override
  public void correctlyCalledButNotSynchronizedVoidMethod(final Integer a, final Integer b) {
    iface.correctlyCalledButNotSynchronizedVoidMethod(a, b);
  }

  @Override
  public void voidMethodNotCalledAtAll(final Integer a, final Integer b) {
    synchronized (this) {
      final int x = 1;
    }
  }

  @Override
  public void voidMethodCalledWithWrongArguments(final Integer a, final Integer b) {
    synchronized (this) {
      iface.voidMethodCalledWithWrongArguments(b, a);
    }
  }

  @Override
  public int correctlyCalledButNotSynchronizedFunction(final int a, final int b) {
    return iface.correctlyCalledButNotSynchronizedFunction(a, b);
  }

  @Override
  public int functionNotCalledAtAll(final int a, final int b) {
    synchronized (this) {
      return 1;
    }
  }

  @Override
  public int functionCalledWithWrongArguments(final int a, final int b) {
    synchronized (this) {
      return iface.functionCalledWithWrongArguments(b, a);
    }
  }

  @Override
  public int functionWithNonPropagatedReturnValue(final int a, final int b) {
    synchronized (this) {
      final int x = iface.functionWithNonPropagatedReturnValue(a, b);
      return Any.intOtherThan(x);
    }
  }

  @Override
  public List<Integer> genericCorrectlyCalledButNotSynchronizedFunction(final int a, final int b) {
    return iface.genericCorrectlyCalledButNotSynchronizedFunction(a, b);
  }

  @Override
  public List<Integer> genericFunctionNotCalledAtAll(final int a, final int b) {
    return new ArrayList<>();
  }

  @Override
  public List<Integer> genericFunctionCalledWithWrongArguments(final int a, final int b) {
    synchronized (this) {
      return iface.genericFunctionCalledWithWrongArguments(b, a);
    }

  }

  @SuppressFBWarnings("RV_RETURN_VALUE_IGNORED_NO_SIDE_EFFECT")
  @Override
  public List<Integer> genericFunctionWithNonPropagatedReturnValue(final Integer a, final Integer b) {
    synchronized (this) {
      iface.genericFunctionWithNonPropagatedReturnValue(a, b);
      return new ArrayList<>();
    }
  }
}
