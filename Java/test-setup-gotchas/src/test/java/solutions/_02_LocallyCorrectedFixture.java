package solutions;

import autofixture.publicinterface.Any;
import lombok.val;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;
import org.whatever.HandRadio;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

public class _02_LocallyCorrectedFixture {

    protected HandRadio radio;
    private String encryptedContent = "43253u45-23487534h-3452873-435234";

    @BeforeMethod
    public void Setup() {
        radio = new HandRadio();
        radio.setFrequency(100);
        radio.turnOn();
        radio.setSecureModeTo(true);
    }

    @Test
    public void ShouldDecryptReceivedContentWhenInSecureMode() {
        //WHEN
        val decryptedContent = radio.receive(encryptedContent);

        //THEN
        assertThat(decryptedContent).isEqualTo("Mayday!");
    }

    //... some tests later...

    @Test
    public void ShouldThrowExceptionWhenTryingToSendWhileItIsNotTurnedOn() {
        //GIVEN
        radio.turnOff(); //undo turning on!!

        //WHEN-THEN
        assertThatThrownBy(() -> radio.send(Any.string()))
            .isInstanceOf(Exception.class);
    }

    @Test
    public void ShouldTransferDataLiterallyWhenReceivingInNonSecureMode() {
        //GIVEN
        radio.setSecureModeTo(false); //undo secure mode setting!!
        val inputSignal = Any.string();

        //WHEN
        val receivedSignal = radio.receive(inputSignal);

        //THEN
        assertThat(receivedSignal).isEqualTo(inputSignal);
    }
}
