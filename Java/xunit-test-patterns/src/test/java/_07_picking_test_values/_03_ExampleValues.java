package _07_picking_test_values;

import lombok.val;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import java.util.stream.Stream;

import static org.assertj.core.api.Assertions.assertThat;

public class _03_ExampleValues {

    public static Stream<Arguments> shouldCorrectlyCheckNamesData() {
        return Stream.of(
            Arguments.of("a", false), //must start with capital letter
            Arguments.of("A"    , false), //cannot be a one-letter name
            Arguments.of("Aa"   , true ), //valid input
            Arguments.of("0"    , false), //cannot have digits
            Arguments.of("&"    , false)  //cannot have special chars
        );
    }

    @ParameterizedTest
    @MethodSource("shouldCorrectlyCheckNamesData")
    public void shouldCorrectlyCheckNames(
        String nameString,
        boolean expectedResult) {

        //GIVEN
        val userNameCheck = new UserNameCheck();
        //WHEN
        val isUserNameValid = userNameCheck.applyTo(nameString);
        //THEN
        assertThat(isUserNameValid).isEqualTo(expectedResult);
    }

}
