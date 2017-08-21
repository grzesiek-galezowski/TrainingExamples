package component;

import autofixture.publicinterface.Any;
import dto.MedicineDto;
import dto.PrescriptionDto;
import lombok.val;

import java.time.LocalDate;
import java.time.Period;
import java.util.Optional;

public class PrescriptionBuilder {
  private LocalDate dispenseDate = Any.localDate();
  private Period daysToSupply = Any.period();

  public PrescriptionDto build() {
    return new PrescriptionDto(dispenseDate, daysToSupply);
  }

  public PrescriptionBuilder from(final LocalDate polopirynaDispenseDate) {
    this.dispenseDate = polopirynaDispenseDate;
    return this;
  }

  public PrescriptionBuilder forPeriodOf(final Period daysToSupplyPolopiryna) {
    this.daysToSupply = daysToSupplyPolopiryna;
    return this;
  }

  public PrescriptionBuilder after(final MedicineDto otherMedicine) {
    val latestPrescription = getPrescriptionFrom(otherMedicine);
    dispenseDate = latestPrescription.get().getDispenseDate().plus(latestPrescription.get().getDaysSupply());
    return this;
  }

  private Optional<PrescriptionDto> getPrescriptionFrom(final MedicineDto otherMedicine) {
    return otherMedicine.getPrescriptionDtos().stream().findFirst();
  }

  public PrescriptionBuilder togetherWith(final MedicineDto otherMed) {
    val latestPrescription = getPrescriptionFrom(otherMed);
    dispenseDate = latestPrescription.get().getDispenseDate();
    daysToSupply = latestPrescription.get().getDaysSupply();
    return this;
  }
}
