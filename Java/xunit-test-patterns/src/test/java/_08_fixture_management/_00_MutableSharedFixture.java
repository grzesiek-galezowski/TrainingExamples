package _08_fixture_management;

import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class _00_MutableSharedFixture {
    //Questions:
    //1. Which one runs first?
    //2. Do both pass?
    private int _i = 0;

    @Test
    public void shouldIncrementANumberOneTime() {
        _i++;
        assertThat(_i).isEqualTo(1);
    }

    @Test
    public void shouldAlsoIncrementANumberOneTime() {
        _i++;
        assertThat(_i).isEqualTo(1);
    }
}
