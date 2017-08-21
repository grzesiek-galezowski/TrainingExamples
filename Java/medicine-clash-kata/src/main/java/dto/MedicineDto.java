package dto;

import lombok.Getter;

import java.util.ArrayList;
import java.util.Collection;

public class MedicineDto {

  @Getter
  private Collection<PrescriptionDto> prescriptionDtos = new ArrayList<PrescriptionDto>();

  @Getter
  private final String name;

  public MedicineDto(String name) {
    this.name = name;
  }

  public void addPrescription(PrescriptionDto prescriptionDto) {
    this.prescriptionDtos.add(prescriptionDto);
  }
}