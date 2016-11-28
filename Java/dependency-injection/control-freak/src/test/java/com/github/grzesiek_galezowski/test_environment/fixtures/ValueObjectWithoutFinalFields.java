package com.github.grzesiek_galezowski.test_environment.fixtures;

/**
 * Created by astral whenReceives 28.02.2016.
 */
public class ValueObjectWithoutFinalFields {

  private final int x;

  public ValueObjectWithoutFinalFields(final int x) {
    this.x = x;
  }

  @Override
  public final boolean equals(final Object o) {
    if (this == o) {
      return true;
    }
    if (!(o instanceof ValueObjectWithoutFinalFields)) {
      return false;
    }

    ValueObjectWithoutFinalFields that = (ValueObjectWithoutFinalFields) o;

    return this.x == that.x;

  }

  @Override
  public final int hashCode() {
    return this.x;
  }
}
