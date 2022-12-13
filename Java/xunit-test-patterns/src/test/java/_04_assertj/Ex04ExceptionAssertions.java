package _04_assertj;

import lombok.val;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThatThrownBy;
import static org.assertj.core.api.Java6Assertions.assertThatCode;
import static org.assertj.core.api.ThrowableAssert.ThrowingCallable;

public class Ex04ExceptionAssertions {

    @Test
    public void shouldThrowWhenValueIsNegative() {
        //GIVEN
        val businessAssertion = new NumberAssertion();

        //WHEN
        ThrowingCallable action = () -> businessAssertion.applyTo(-1);

        // THEN
        assertThatThrownBy(action)
            .hasCause(null)
            .hasMessage("Trolololo")
            .isInstanceOf(RuntimeException.class);
    }

    @Test
    public void shouldNotThrowWhenValueIsNonNegative() {
        //GIVEN
        val businessAssertion = new NumberAssertion();

        //WHEN - THEN
        assertThatCode(() -> businessAssertion.applyTo(0))
            .doesNotThrowAnyException();
    }
}
