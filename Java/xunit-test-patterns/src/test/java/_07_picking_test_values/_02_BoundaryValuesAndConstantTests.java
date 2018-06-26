package _07_picking_test_values;

import _03_parameterized_tests.Person;
import lombok.val;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class _02_BoundaryValuesAndConstantTests {
    @DataProvider
    public static Object[][] shouldBeAdultDependingOnAgeData() {
        return new Object[][]{
            {Person.ADULT_AGE - 1, false},
            {Person.ADULT_AGE, true},
        };
    }

    @Test(dataProvider = "shouldBeAdultDependingOnAgeData")
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
