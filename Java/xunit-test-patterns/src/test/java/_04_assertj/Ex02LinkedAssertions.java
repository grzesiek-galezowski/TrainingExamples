package _04_assertj;

import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex02LinkedAssertions {
    @Test
    public void trololololo1() {
        assertThat(1).isEqualTo(1);
        assertThat(1).isGreaterThan(0);
        assertThat(1).isLessThan(2);
        assertThat(1).isEqualTo(2);
    }

    @Test
    public void trololololo2() {
        assertThat(1)
            .isEqualTo(1)
            .isGreaterThan(0)
            .isLessThan(2)
            .isEqualTo(2);
    }
}
