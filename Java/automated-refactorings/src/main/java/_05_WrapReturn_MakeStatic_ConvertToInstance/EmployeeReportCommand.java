package _05_WrapReturn_MakeStatic_ConvertToInstance;

import java.util.ArrayList;

public class EmployeeReportCommand {
  private int averagePay;
  private ArrayList<EmployeeDto> employeeDtos;

  public EmployeeReportCommand(final int averagePay) {
    this.averagePay = averagePay;
  }

  //todo apply to report
  //todo apply to employee dto
  //todo apply to employees
  //1. extract method
  //2. wrap return value
  //3. extract variable
  //4. inline
  //5. Extract method
  //6. Make static
  //7. Convert to instance method
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
