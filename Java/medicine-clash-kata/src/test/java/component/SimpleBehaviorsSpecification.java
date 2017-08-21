package component;

import api.Patient;
import autofixture.publicinterface.Any;
import dto.MedicineDto;
import lombok.val;
import org.testng.annotations.Test;

import java.time.Period;
import java.util.Arrays;
import java.util.function.Consumer;

import static org.assertj.core.api.Assertions.assertThat;

public class SimpleBehaviorsSpecification {
  @Test
  public void shouldNotDiscoverClashesWhenThereAreNoMedicines() {
    //GIVEN
    val patient = new Patient();

    //WHEN
    val clashes = patient.clash(Arrays.asList());

    //THEN
    assertThat(clashes).isEmpty();
  }

  @Test
  public void shouldNotDiscoverClashesWhenThereAreOnlyMedicinesWithoutPrescriptions() {
    //GIVEN
    val patient = new Patient();
    val polopiryna = medicine().build();
    val polopirynaS = medicine().build();
    patient.addMedicine(polopiryna);
    patient.addMedicine(polopirynaS);

    //WHEN
    val clashes = patient.clash(
        Arrays.asList(
            polopiryna.getName(),
            polopirynaS.getName()));

    //THEN
    assertThat(clashes).isEmpty();
  }

  @Test
  public void shouldNotDiscoverClashesWhenThereAreMedicinesWithNoOverlappingPrescriptions() {
    //GIVEN
    val patient = new Patient();
    val polopiryna = medicine()
        .prescribed(however()).build();
    val polopirynaS = medicine()
        .prescribed(after(polopiryna)).build();

    patient.addMedicine(polopiryna);
    patient.addMedicine(polopirynaS);

    //WHEN
    val clashes = patient.clash(
        Arrays.asList(
            polopiryna.getName(),
            polopirynaS.getName()));

    //THEN
    assertThat(clashes).isEmpty();
  }
  @Test
  public void shouldDiscoverClashWhenTwoMedicinesPrescriptionsOverlapFor5Days() {
    //GIVEN
    val fiveDays = Period.ofDays(5);
    val patient = new Patient();
    val dispenseDate = Any.localDate();
    val polopiryna = medicine()
        .prescribed(p -> p.from(dispenseDate).forPeriodOf(fiveDays)).build();
    val polopirynaS = medicine()
        .prescribed(togetherWith(polopiryna)).build();

    patient.addMedicine(polopiryna);
    patient.addMedicine(polopirynaS);

    //WHEN
    val clashes = patient.clash(
        Arrays.asList(
            polopiryna.getName(),
            polopirynaS.getName()));

    //THEN
    assertThat(clashes)
        .hasSize(5)
        .contains(dispenseDate)
        .contains(dispenseDate.plus(Period.ofDays(1)))
        .contains(dispenseDate.plus(Period.ofDays(2)))
        .contains(dispenseDate.plus(Period.ofDays(3)))
        .contains(dispenseDate.plus(Period.ofDays(4)))
        .isEmpty();
  }

  private Consumer<PrescriptionBuilder> togetherWith(final MedicineDto otherMed) {
    return p -> p.togetherWith(otherMed);
  }

  private Consumer<PrescriptionBuilder> however() {
    return p -> {};
  }

  private Consumer<PrescriptionBuilder> after(final MedicineDto polopiryna) {
    return p -> p.after(polopiryna);
  }

  private MedicineBuilder medicine() {
    return new MedicineBuilder();
  }

}
