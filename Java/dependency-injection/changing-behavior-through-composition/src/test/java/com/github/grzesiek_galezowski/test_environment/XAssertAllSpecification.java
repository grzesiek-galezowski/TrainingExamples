package com.github.grzesiek_galezowski.test_environment;

import org.assertj.core.api.Assertions;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class XAssertAllSpecification {

  public static final int ARBITRARY_NUMBER = 12;

  @Test
  public void shouldAllowMakingSoftAssertionsAndThrowAtTheEnd() {

    Assertions.assertThatThrownBy(() ->
        XAssert.assertAll(softly -> {
          softly.assertThat(ARBITRARY_NUMBER).isEqualTo(ARBITRARY_NUMBER + 1);
          softly.assertThat(true).isEqualTo(false);
          softly.assertThat("Johnny").isEqualTo("Johnny");
        }))
        .hasMessageContaining("The following 2 assertions failed:");
  }

  @Test
  public void shouldAllowMakingSoftAssertionsAndNotThrowAtTheEndWhenNoAssertionsFail() {

    final Throwable exception = Assertions.catchThrowable(() ->
        XAssert.assertAll(softly -> {
          softly.assertThat(ARBITRARY_NUMBER).isEqualTo(ARBITRARY_NUMBER);
          softly.assertThat(true).isEqualTo(true);
          softly.assertThat("Johnny").isEqualTo("Johnny");
        }));

    assertThat(exception).isEqualTo(null);
  }

}