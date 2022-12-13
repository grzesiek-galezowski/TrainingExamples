package _05_custom_assertions;

import com.github.grzesiek_galezowski.test_environment.XAssert;
import lombok.val;
import org.assertj.core.api.SoftAssertions;
import org.junit.jupiter.api.Test;

public class Ex04SolutionWithCustomAndSoftAssertions {
    static class PersonData {
        public final String name;
        public final String surname;
        public final int age;

        PersonData(String name, String surname, int age) {
            this.name = name;
            this.surname = surname;
            this.age = age;
        }

        public static PersonData clarkKent() {
            return new PersonData("Clark", "Kent", 35);
        }

        public static PersonData superman() {
            return new PersonData("Clark", "Kent", 35);
        }
    }

    @Test
    public void shouldCreateSupermanWithIdenticalDataAsClarkKent() {
        //GIVEN
        val clark = PersonData.clarkKent();

        //WHEN
        val superman = PersonData.superman();

        //THEN
        assertThatAreTheSamePerson(superman, clark);
    }

    private void assertThatAreTheSamePerson(PersonData superman, PersonData clark) {
        XAssert.assertAll(softly -> {
            softly.assertThat(superman.name).as("name").isEqualTo(clark.name);
            softly.assertThat(superman.surname).as("surname").isEqualTo(clark.surname);
            softly.assertThat(superman.age).as("age").isEqualTo(clark.age);
        });
    }

    private void assertThatAreTheSamePerson2(PersonData superman, PersonData clark) {
        val softly = new SoftAssertions();
        softly.assertThat(superman.name).as("name").isEqualTo(clark.name);
        softly.assertThat(superman.surname).as("surname").isEqualTo(clark.surname);
        softly.assertThat(superman.age).as("age").isEqualTo(clark.age);
        softly.assertAll();
    }

}
