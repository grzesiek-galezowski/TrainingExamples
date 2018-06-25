package _04_assertj;

import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex03FailureMessagesAndDescriptions {

    @Test
    public void trolololo() {
        assertThat(2)
            .as("Simple numbers 1")            // description
            .withFailMessage("2 was expected") // fail message
            .isEqualTo(2);

        assertThat(1)
            .as("Simple numbers 2")             // description
            .withFailMessage("2 was expected")  // fail message
            .isEqualTo(2);

        assertThat(0)
            .as("Simple numbers 3")             // description
            .withFailMessage("2 was expected")  // fail message
            .isEqualTo(2);

    }
}
