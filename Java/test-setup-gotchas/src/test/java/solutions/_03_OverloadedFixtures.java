package solutions;

import org.junit.Test;

import static org.assertj.core.api.Assertions.assertThat;

//!!!
public class _03_OverloadedFixtures extends _02_LocallyCorrectedFixture {

    @Test
    public void ShouldAllowGettingFrequencyThatWasSet() {
        //GIVEN
        this.radio.setFrequency(220);

        //WHEN
        int frequency = radio.getFrequency();

        //THEN
        //does it depend on the radio being turned on?
        assertThat(frequency).isEqualTo(220);
    }
}
