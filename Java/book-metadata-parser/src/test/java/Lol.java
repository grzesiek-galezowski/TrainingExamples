import org.junit.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Lol {
    @Test
    public void tryOut() {
        assertThat("1").isNotNull();
        assertThat("").isNull();
    }
}
