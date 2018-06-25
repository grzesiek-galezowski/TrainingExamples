package _02_basics;

import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex01FourPhaseTest {

    @Test
    public void shouldAllowAccessingItsName() {
        //GIVEN
        String anyName = "Zenek";
        User user = new User(anyName, "Ziomal");

        //WHEN
        String name = user.getName();

        //THEN
        assertThat(name).isEqualTo(anyName);
    }
    //TODO where is the 4th phase?

    @Test
    public void shouldAllowAccessingItsName2() {
        //we don't use this convention
        //ARRANGE
        String anyName = "Zenek";
        User user = new User(anyName, "Ziomal");

        //ACT
        String name = user.getName();

        //ASSERT
        assertThat(name).isEqualTo(anyName);

        //ANNIHILATE
        //...
    }
}
