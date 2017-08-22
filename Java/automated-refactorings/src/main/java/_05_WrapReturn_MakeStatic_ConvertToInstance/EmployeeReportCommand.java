package _05_WrapReturn_MakeStatic_ConvertToInstance;

import java.util.ArrayList;

public class EmployeeReportCommand {
  private int averagePay;
  private ArrayList<EmployeeDto> employeeDtos;

  public EmployeeReportCommand(final int averagePay) {
    this.averagePay = averagePay;
  }

  public void execute() {
    final ArrayList<EmployeeDto> employeeDtos = Database.loadEmployees();
    String report = "";
    report += "=== BEGIN ===" + System.lineSeparator();

    report += "UNDERPAID EMPLOYEES" + System.lineSeparator();

    //underpaid employees
    for(EmployeeDto e : employeeDtos) {
      if(e.getPay() < averagePay * 0.8) {
        report += e.toString() + System.lineSeparator();
      }
    }

    report += "OVERPAID EMPLOYEES" + System.lineSeparator();

    //overpaid employees
    for(EmployeeDto e : employeeDtos) {
      if(e.getPay() > averagePay * 1.2) {
        report += e.toString() + System.lineSeparator();
      }
    }

    report += "=== END ===" + System.lineSeparator();


  }
}
