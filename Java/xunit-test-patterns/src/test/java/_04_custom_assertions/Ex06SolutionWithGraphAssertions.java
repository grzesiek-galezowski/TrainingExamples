package _04_custom_assertions;

import lombok.val;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex06SolutionWithGraphAssertions {
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
            return new PersonData("Clark", "Kent", 351);
        }
    }

    @Test
    public void shouldCreateSupermanWithIdenticalDataAsClarkKent() {
        //GIVEN
        val clark = PersonData.clarkKent();

        //WHEN
        val superman = PersonData.superman();

        //THEN
        assertThat(superman).isEqualToComparingFieldByFieldRecursively(clark);
    }

}
