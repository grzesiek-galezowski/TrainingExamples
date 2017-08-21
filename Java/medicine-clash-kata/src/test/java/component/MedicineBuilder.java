package component;

import autofixture.publicinterface.Any;
import dto.MedicineDto;
import dto.PrescriptionDto;
import lombok.val;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Consumer;

public class MedicineBuilder {
  private List<PrescriptionDto> prescriptions = new ArrayList<>();

  MedicineDto build() {
    MedicineDto anonymous = Any.anonymous(MedicineDto.class);
    for (val prescription : prescriptions) {
      anonymous.addPrescription(prescription);
    }
    return anonymous;
  }

  public MedicineBuilder prescribed(final Consumer<PrescriptionBuilder> actions) {
    PrescriptionBuilder prescriptionBuilder = new PrescriptionBuilder();
    actions.accept(prescriptionBuilder);
    prescriptions.add(prescriptionBuilder.build());
    return this;
  }
}
