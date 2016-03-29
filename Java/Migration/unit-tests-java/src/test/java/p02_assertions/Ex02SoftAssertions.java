package p02_assertions;

import com.github.grzesiek_galezowski.test_environment.XAssert;
import org.assertj.core.api.SoftAssertions;
import org.testng.annotations.Test;

import static com.github.grzesiek_galezowski.test_environment.XAssert.assertAll;

public class Ex02SoftAssertions {
  @Test
  public void shouldSoftlyMatchWithJAssertApi() {
    SoftAssertions softly = new SoftAssertions();
    softly.assertThat(2).isEqualTo(2);
    softly.assertThat(4).isEqualTo(4);
    softly.assertThat(6).isEqualTo(6);
    softly.assertAll();
  }

  @Test
  public void shouldSoftlyMatchWithTddToolkitApi() {
    XAssert.assertAll(softly -> {
      softly.assertThat(2).isEqualTo(2);
      softly.assertThat(4).isEqualTo(4);
      softly.assertThat(6).isEqualTo(6);
    });

    //static import
    assertAll(softly -> {
      softly.assertThat(2).isEqualTo(2);
      softly.assertThat(4).isEqualTo(4);
      softly.assertThat(6).isEqualTo(6);
    });
  }

}
