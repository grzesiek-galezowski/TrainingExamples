
//todo add infinitest facet to test project
//todo add test path to compile path
//todo restart
//todo build / rebuild

import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

/**
 * Created by ftw637 on 11/17/2015.
 */
public class Specification {

    @Test
    public void shouldXyz() {
        assertThat(6).isEqualTo(6);
    }
}
