package _04_assertj;

import org.testng.annotations.Test;

import javax.jws.WebService;

import static com.google.common.collect.Lists.newArrayList;
import static org.assertj.core.api.Assertions.assertThat;

@WebService
public class Ex01TypeSpecificAssertions {
    @Test
    public void trololololo() {
        assertThat(3)
            .isGreaterThan(2);
        assertThat("  ")
            .containsOnlyWhitespaces();
        assertThat(newArrayList(1,2,3))
            .containsSubsequence(1,2);
        assertThat(this.getClass())
            .hasAnnotations(WebService.class);
    }
}
