package _05_assertj;

import org.junit.jupiter.api.Test;

import java.util.Optional;

import static com.google.common.collect.Lists.newArrayList;
import static org.assertj.core.api.Assertions.assertThat;

@Deprecated
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
            .hasAnnotations(Deprecated.class);

        assertThat(Optional.of("lol"))
            .isNotEmpty()
            .hasValue("lol");
    }
}
