package dto;

import lombok.Getter;

import java.time.LocalDate;
import java.time.Period;

public class PrescriptionDto {

  @Getter
  private LocalDate dispenseDate;
  @Getter
  private Period daysSupply = Period.ofDays(30);

  public PrescriptionDto(LocalDate dispenseDate, Period daysSupply) {
    this.dispenseDate = dispenseDate;
    this.daysSupply = daysSupply;
  }

}