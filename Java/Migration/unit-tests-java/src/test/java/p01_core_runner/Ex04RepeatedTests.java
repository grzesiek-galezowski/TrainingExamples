package p01_core_runner;

import org.testng.annotations.Test;

import java.util.concurrent.ThreadLocalRandom;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex04RepeatedTests {
  private final ThreadLocalRandom current = ThreadLocalRandom.current();

  //non-deterministic test
  @Test//(invocationCount = 100) //running multiple times helps in diagnosis:
  public void shouldBeGreaterThan10() {
    //GIVEN
    int buggyVariable = current.nextInt(0, 100);

    //THEN
    assertThat(buggyVariable).isGreaterThan(10); //low chance of happening
  }
}
