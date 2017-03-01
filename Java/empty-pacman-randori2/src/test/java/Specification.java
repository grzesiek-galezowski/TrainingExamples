import org.junit.Test;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.core.IsEqual.equalTo;

//todo add infinitest facet to test project
//todo add test path to compile path
//todo restart
//todo build / rebuild

/**
 * Created by ftw637 on 11/17/2015.
 */
public class Specification {

    @Test
    public void shouldXyz() {
        assertThat(6, equalTo(32) );
    }
}
