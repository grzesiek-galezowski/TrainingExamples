package solutions;

import org.junit.Test;
import org.testng.annotations.BeforeMethod;
import org.whatever.UserRegistration;

import static org.assertj.core.api.Assertions.assertThat;

public class _04_EvadingAlreadyUsedValues {
    private UserRegistration registration;

    @BeforeMethod
    public void Setup() {
        registration = new UserRegistration();
        registration.performFor("user1");

        //let's assume this is for other tests...
        registration.performFor("user2");
        registration.performFor("anotherUser");
        registration.performFor("Jenny");
        registration.performFor("Johnny");
        registration.performFor("myUser");
        registration.performFor("userInOrganization");
        registration.performFor("userWithoutPrivileges");

    }

    @Test
    public void ShouldBeActiveForRegisteredUsers() {
        assertThat(registration.isActiveFor("user1")).isTrue();
    }

    @Test
    public void ShouldBeInactiveForUnregisteredUsers() {
        assertThat(
            //must evade all used values or it fails!
            registration.isActiveFor("unregisteredUser")).isFalse();
    }
}
