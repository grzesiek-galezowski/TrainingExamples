package _07_picking_test_values;

import _03_parameterized_tests.Person;
import lombok.val;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;
import org.junit.jupiter.api.Test;

import java.util.stream.Stream;

import static org.assertj.core.api.Assertions.assertThat;

public class _02_BoundaryValuesAndConstantTests {
    public static Stream<Arguments> shouldBeAdultDependingOnAgeData() {
        return Stream.of(
          Arguments.of(Person.ADULT_AGE - 1, false),
          Arguments.of(Person.ADULT_AGE, true)
        );
    }

    @ParameterizedTest
    @MethodSource("shouldBeAdultDependingOnAgeData")
    public void shouldBeAdultDependingOnAge(int age, boolean expectedIsAdult) {
        //GIVEN
        val person = new Person(age);

        //WHEN
        val isAdult = person.isAdult();

        //THEN
        assertThat(isAdult).isEqualTo(expectedIsAdult);
    }

    @Test
    public void shouldSayThatAdultAgeIs18() {
        assertThat(Person.ADULT_AGE).isEqualTo(18);
    }

}
