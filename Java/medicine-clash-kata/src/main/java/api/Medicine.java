package api;

import dto.MedicineDto;
import lombok.val;

import java.time.LocalDate;
import java.util.Collection;

public class Medicine {
  private final MedicineDto value;

  public Medicine(final MedicineDto value) {
    this.value = value;
  }

  public MedicineDto getValue() {
    return value;
  }

  public boolean isAnyOf(final Collection<String> medicineNames) {
    return true;
  }

  public void addClashesWith(final Medicine med2, final Collection<LocalDate> clashes) {
    for(val prescriptionDto : value.getPrescriptionDtos()) {
      med2.addClashesWith(new Prescription(prescriptionDto), clashes);
    }
  }

  public void addClashesWith(final Prescription prescription, final Collection<LocalDate> clashes) {
    for(val prescriptionDto : value.getPrescriptionDtos()) {
      new Prescription(prescriptionDto).addClashesWith(prescription, clashes);
    }
  }
}
