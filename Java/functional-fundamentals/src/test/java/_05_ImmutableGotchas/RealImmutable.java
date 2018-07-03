package _05_ImmutableGotchas;

import org.junit.Test;

import java.time.ZonedDateTime;

import static org.mutabilitydetector.unittesting.MutabilityAssert.assertImmutable;
import static org.mutabilitydetector.unittesting.MutabilityAssert.assertInstancesOf;
import static org.mutabilitydetector.unittesting.MutabilityMatchers.areEffectivelyImmutable;

public class RealImmutable {

    @Test
    public void failsBecauseClassIsNotFinal() {
        assertImmutable(NonFinalEmptyObject.class);
    }

    @Test
    public void passesBecauseClassIsFinal() {
        assertImmutable(FinalEmptyObject.class);
    }

    @Test
    public void failsBecauseFieldIsNotFinalEvenThoughTheClassIsEffectivelyImmutable() {
        assertInstancesOf(ObjectWithSinglePrivateField.class, areEffectivelyImmutable());
        assertImmutable(ObjectWithSinglePrivateField.class);
    }

    @Test
    public void passesBecauseFieldIsFinal() {
        assertImmutable(ObjectWithSinglePrivateFinalField.class);
    }

    @Test
    public void passesDespiteProtectedFieldBecauseTheClassIsFinal() {
        assertImmutable(ObjectWithSingleFinalProtectedField.class);
    }
    @Test
    public void failsBecauseCollectionsAreMutable() {
        assertImmutable(ObjectWithPrivateFinalListFieldAndGetter.class);
    }

    @Test
    public void failsBecauseCollectionsAreMutable2() {
        assertImmutable(ObjectWithPrivateFinalListPassedViaConstructor.class);
    }

    @Test
    public void failsBecauseDatesAreMutable() {
        assertImmutable(ObjectWithPrivateFinalDateField.class);
    }

    @Test
    public void failsBecauseJavaMemoryModelCantGuaranteeImmutability() {
        assertImmutable(ObjectWithPrivateNonFinalDateWithZoneField.class);
    }

    @Test
    public void failsBecauseZonedDateTimeUsesAbstractFields() {
        assertImmutable(ObjectWithPrivateFinalDateWithZoneField.class);
        assertImmutable(ZonedDateTime.class);
    }


}
