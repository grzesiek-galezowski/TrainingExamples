package _08_fixture_management;

import autofixture.publicinterface.Any;
import lombok.val;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class _04_TestDataBuilders {

    @Test
    public void shouldIncludeUserNameInAppendedUserLog() {
        //GIVEN
        val user = new UserBuilder().withName("Zenek").build();
        val inMemoryLog = new InMemoryLog();

        //WHEN
        inMemoryLog.logUser(user);

        //THEN
        assertThat(inMemoryLog.entries())
            .last().asString().contains("Zenek");
    }

    static final class UserBuilder {
        private String name = Any.string();
        private String surname = Any.string();
        private int age = Any.intValue();

        public UserBuilder withName(String name) {
            this.name = name;
            return this;
        }

        public UserBuilder withSurname(String surname) {
            this.surname = surname;
            return this;
        }

        public UserBuilder withAge(int age) {
            this.age = age;
            return this;
        }

        public User build() {
            return new User(name, surname, age);
        }
    }

}
