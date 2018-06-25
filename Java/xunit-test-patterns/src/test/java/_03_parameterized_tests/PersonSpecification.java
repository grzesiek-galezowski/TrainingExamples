package _03_parameterized_tests;

import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class PersonSpecification {

    @DataProvider
    public static Object[][] shouldBeAdultDependingOnAgeData() {
        return new Object[][]{
            {17, false},
            {18, true},
        };
    }

    @Test(dataProvider = "shouldBeAdultDependingOnAgeData")
    public void shouldBeAdultDependingOnAge(int age, boolean expectedIsAdult) {
        //GIVEN
        Person person = new Person(age);

        //WHEN
        boolean isAdult = person.isAdult();

        //THEN
        assertThat(isAdult).isEqualTo(expectedIsAdult);
    }

}
