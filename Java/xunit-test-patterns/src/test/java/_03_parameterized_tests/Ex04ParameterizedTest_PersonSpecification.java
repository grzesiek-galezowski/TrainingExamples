package _03_parameterized_tests;

import lombok.val;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import java.util.stream.Stream;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex04ParameterizedTest_PersonSpecification {

    public static Stream<Arguments> shouldBeAdultDependingOnAgeData() {
        return Stream.of(
            Arguments.of(17, false),
            Arguments.of(18, true)
        );
    }

    @ParameterizedTest //(name = "lol {0} {1}")
    @MethodSource("shouldBeAdultDependingOnAgeData")
    public void shouldBeAdultDependingOnAge(int age, boolean expectedIsAdult) {
        //GIVEN
        val person = new Person(age);

        //WHEN
        val isAdult = person.isAdult();

        //THEN
        assertThat(isAdult).isEqualTo(expectedIsAdult);
    }

}
