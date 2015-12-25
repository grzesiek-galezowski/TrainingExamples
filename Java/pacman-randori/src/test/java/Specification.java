import org.junit.Test;

import static org.hamcrest.CoreMatchers.equalTo;
import static org.hamcrest.CoreMatchers.is;
import static org.junit.Assert.assertThat;

/**
 * Created by ftw637 on 11/17/2015.
 */
public class Specification {

    @Test
    public void shouldXyz() {
        assertThat(2, is(equalTo(123)));
        assertThat(2, is(equalTo(123)));
    }
}
