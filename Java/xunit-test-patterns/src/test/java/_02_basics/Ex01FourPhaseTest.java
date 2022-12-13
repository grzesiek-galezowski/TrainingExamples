package _02_basics;

import lombok.val;
import org.junit.jupiter.api.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex01FourPhaseTest {

    @Test
    public void shouldAllowAccessingItsName() {
        //GIVEN
        val anyName = "Zenek";
        val user = new User(anyName, "Ziomal");

        //WHEN
        val name = user.getName();

        //THEN
        assertThat(name).isEqualTo(anyName);
    }
    //TODO where is the 4th phase?

    @Test
    public void shouldAllowAccessingItsName2() {
        //we don't use this convention
        //ARRANGE
        val anyName = "Zenek";
        val user = new User(anyName, "Ziomal");

        //ACT
        val name = user.getName();

        //ASSERT
        assertThat(name).isEqualTo(anyName);

        //ANNIHILATE
        //...
    }
}
