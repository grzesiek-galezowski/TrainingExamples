package _05_custom_assertions;

import lombok.val;
import org.assertj.core.api.Condition;
import org.assertj.core.api.SoftAssertions;
import org.testng.annotations.Test;

import static java.util.stream.Collectors.joining;
import static org.assertj.core.api.Assertions.assertThat;

public class Ex05SolutionWithCustomConditions {
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
        assertThat(superman).is(theSamePersonAs(clark));
    }

    private Condition<? super PersonData> theSamePersonAs(PersonData clark) {
        return new PersonEqualityCondition(clark);
    }

    static class PersonEqualityCondition extends Condition<PersonData> {
        private PersonData expected;

        public PersonEqualityCondition(PersonData expected) {
            this.expected = expected;
        }

        @Override
        public boolean matches(PersonData actual) {
            val softly = new SoftAssertions();
            softly.assertThat(actual.name).as("name").isEqualTo(expected.name);
            softly.assertThat(actual.surname).as("surname").isEqualTo(expected.surname);
            softly.assertThat(actual.age).as("age").isEqualTo(expected.age);

            if (!softly.errorsCollected().isEmpty()) {
                describedAs(errorsFrom(softly));
                return false;
            } else {
                return true;
            }
        }

        private String errorsFrom(SoftAssertions softly) {
            return "equal to expected, but " + softly.errorsCollected().stream()
                .map(t -> t.getMessage())
                .collect(joining("\n"));
        }
    }

}
