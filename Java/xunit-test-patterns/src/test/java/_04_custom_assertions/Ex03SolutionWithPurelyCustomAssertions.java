package _04_custom_assertions;

import lombok.val;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex03SolutionWithPurelyCustomAssertions {
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
        assertThat(superman.name).as("name").isEqualTo(clark.name);
        assertThat(superman.surname).as("surname").isEqualTo(clark.surname);
        assertThat(superman.age).as("age").isEqualTo(clark.age);
    }


}
