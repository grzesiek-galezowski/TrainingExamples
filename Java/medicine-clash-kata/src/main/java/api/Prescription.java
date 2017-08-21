package api;

import dto.PrescriptionDto;
import lombok.val;

import java.time.Duration;
import java.time.LocalDate;
import java.time.Period;
import java.util.Collection;

public class Prescription {
  private PrescriptionDto prescriptionDto;

  public Prescription(
      final PrescriptionDto prescriptionDto) {
    this.prescriptionDto = prescriptionDto;
  }

  public void addClashesWith(
      final Prescription prescription,
      final Collection<LocalDate> clashes) {
    prescription.addClashesWith(
        prescriptionDto.getDispenseDate(),
        prescriptionDto.getDaysSupply());
  }

  private void addClashesWith(final LocalDate dispenseDate, final Period daysSupply) {
     int numberOfDays = daysSupply.getDays();
     for(int currentDay = 0 ; currentDay < numberOfDays ; currentDay++) {
       val currentDate = dispenseDate.plus(Duration.ofDays(currentDay));

     }
  }
}
