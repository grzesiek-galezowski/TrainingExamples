package com.github.grzesiek_galezowski.test_environment;

import com.github.grzesiek_galezowski.test_environment.implementation_details.SynchronizationAssertDsl;
import nl.jqno.equalsverifier.EqualsVerifier;
import org.assertj.core.api.SoftAssertions;
import org.assertj.core.api.ThrowableAssert.ThrowingCallable;

import java.util.function.Consumer;

import static org.assertj.core.api.Assertions.catchThrowable;
import static org.assertj.core.api.AssertionsForInterfaceTypes.assertThat;

public class XAssert {

  public static void assertAll(final Consumer<SoftAssertions> assertions) {
    final SoftAssertions softAssertions = new SoftAssertions();
    assertions.accept(softAssertions);
    softAssertions.assertAll();
  }

  public static void assertThatNotThrownBy(final ThrowingCallable callable) {
    final Throwable exception = catchThrowable(callable);
    assertThat(exception).isEqualTo(null);
  }

  public static <T> void assertValueObject(final Class<T> clazz) {
    EqualsVerifier.forClass(clazz).verify();
  }

  public static <T> SynchronizationAssertDsl<T> assertThatProxyTo(final T wrappedMock, final T realThing) {
    return new SynchronizationAssertDsl<>(wrappedMock, realThing);
  }
}

