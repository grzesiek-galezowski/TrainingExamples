package api;

import dto.MedicineDto;
import lombok.val;

import java.time.LocalDate;
import java.time.Period;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

public class Patient {

  private List<Medicine> medicines = new ArrayList<>();

  public void addMedicine(MedicineDto medicineDto) {
    this.medicines.add(createMedicine(medicineDto));
  }

  private Medicine createMedicine(final MedicineDto medicineDto) {
    return new Medicine(medicineDto);
  }

  public Collection<LocalDate> clash(Collection<String> medicineNames) {
    return clash(medicineNames, Period.ofDays(90));
  }

  //fixme add test for different names
  public Collection<LocalDate> clash(Collection<String> medicineNames, Period daysBack) {
    Collection<LocalDate> clashes = new ArrayList<>();
    for(int i = 0; i < medicines.size() ; ++i) {
      for(int j = 0 ; j < medicines.size() ; ++j) {
        if(i != j) {
          val med1 = medicines.get(i);
          val med2 = medicines.get(j);
          med1.addClashesWith(med2, clashes);
        }
      }
    }
    return clashes;
  }


}