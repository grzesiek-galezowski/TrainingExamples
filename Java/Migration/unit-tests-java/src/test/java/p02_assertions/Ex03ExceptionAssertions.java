package p02_assertions;

import com.github.grzesiek_galezowski.test_environment.XAssert;
import org.testng.annotations.Test;

import static org.assertj.core.api.AssertionsForClassTypes.assertThatThrownBy;

public class Ex03ExceptionAssertions {

  @Test
  public void ShouldShowThrownAssertions()
  {
    assertThatThrownBy(() -> doSomethingThatThrows())
        .isInstanceOf(RuntimeException.class)
        .hasMessage("Tralala");
  }

  private void doSomethingThatThrows()
  {
    throw new RuntimeException("Tralala");
  }

  @Test
  public void ShouldShowNotThrownAssertions()
  {
    XAssert.assertThatNotThrownBy(() -> doSomethingThatDoesNotThrow());
  }

  private void doSomethingThatDoesNotThrow()
  {
  }


}
