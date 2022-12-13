package _05_custom_assertions;

import lombok.val;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex01NoTrueFalseAssertions {

    @Test
    public void usingAssertTrue() {
        //show assertion errors
        assertThat(1 == 4).isTrue();
    }

    @Test
    public void usingSpecificAssertion() {
        //show assertion errors
        assertThat(1).isEqualTo(4);
    }

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
        assertThat(areTheSame(clark, superman)).isTrue();
    }

    private boolean areTheSame(PersonData clark, PersonData superman) {
        return clark.age == superman.age
            && clark.name.equals(superman.name)
            && clark.surname.equals(superman.surname);
    }
}
