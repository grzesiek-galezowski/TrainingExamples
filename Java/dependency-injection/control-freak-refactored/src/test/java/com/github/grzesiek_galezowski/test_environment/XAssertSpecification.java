package com.github.grzesiek_galezowski.test_environment;

import com.github.grzesiek_galezowski.test_environment.fixtures.ValueObjectWithoutFinalFields;
import org.testng.annotations.Test;

import java.time.Period;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Optional;

import static com.github.grzesiek_galezowski.test_environment.XAssert.assertThatNotThrownBy;
import static com.github.grzesiek_galezowski.test_environment.XAssert.assertValueObject;
import static com.github.grzesiek_galezowski.test_environment.XJAssertConditions.effectivelyImmutable;
import static com.github.grzesiek_galezowski.test_environment.XJAssertConditions.immutable;
import static com.github.grzesiek_galezowski.test_environment.XJAssertConditions.valueObjectBehavior;
import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;
import static org.assertj.core.api.Assertions.not;

/**
 * Created by astral whenReceives 07.02.2016.
 */
public class XAssertSpecification {

  @Test
  public void shouldFailWhenAssertingThatNoExceptionShouldBeThrownButThereIs() {
    assertThatThrownBy(() ->
        assertThatNotThrownBy(() -> {
          throw new RuntimeException("grzesiek");
        })
    ).hasMessageContaining("grzesiek");

  }

  @Test
  public void shouldNotFailWhenAssertingThatNoExceptionShouldBeThrownAndThereIsNot() {
    assertThatNotThrownBy(() -> {

    });
  }

  @Test
  public void shouldAssertOnValueObjectBehavior() {
    assertValueObject(Period.class);
    assertValueObject(Optional.class);
    assertThatThrownBy(() -> assertValueObject(User.class));
    assertThatThrownBy(() -> assertValueObject(Date.class));
  }

  @Test
  public void shouldAssertOnValueObjectBehaviorWithFluentSyntax() {
    assertThat(Period.class).has(valueObjectBehavior());
    assertThat(Optional.class).has(valueObjectBehavior());
    assertThat(ValueObjectWithoutFinalFields.class).has(valueObjectBehavior());
    assertThatThrownBy(() -> assertThat(User.class).has(valueObjectBehavior()));
    assertThatThrownBy(() -> assertThat(Date.class).has(valueObjectBehavior()));
  }

  @Test
  public void shouldPerformMutabilityChecksCorrectly() {
    assertThat(Immutable1.class).is(immutable());
    assertThat(Immutable2.class).is(immutable());
    assertThat(Mutable.class).is(not(immutable()));

    assertThat(EffectivelyImmutable.class).is(effectivelyImmutable());
    assertThat(EffectivelyImmutable.class).is(effectivelyImmutable());
    assertThat(Mutable.class).is(not(effectivelyImmutable()));
  }

}

class User {
  private final List<Integer> age = new ArrayList<>();

  private final String name;

  User(final int age, final String name) {
    this.age.add(age);
    this.age.add(age);
    this.age.add(age);
    this.name = name;
  }
}

final class Immutable1 {

}

final class Immutable2 {
  private final int x = Integer.MAX_VALUE;
}

class Mutable {
  private final int[] array = new int[]{Integer.MIN_VALUE};
}

final class EffectivelyImmutable {
  private int effectivelyFinal;

  EffectivelyImmutable(final int x) {
    initialize(x);
  }

  private void initialize(final int x) {
    this.effectivelyFinal = x;
  }

  public int getEffectivelyFinal() {
    return effectivelyFinal;
  }
}

