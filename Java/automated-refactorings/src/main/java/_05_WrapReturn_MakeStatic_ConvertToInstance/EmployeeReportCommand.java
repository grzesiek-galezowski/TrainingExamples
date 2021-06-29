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
    StringBuilder report = new StringBuilder("");

    //report header
    report.append("=== BEGIN ===").append(System.lineSeparator());

    //underpaid employees header
    report.append("UNDERPAID EMPLOYEES").append(System.lineSeparator());

    //underpaid employees
    for(EmployeeDto e : employeeDtos) {
      if(e.getPay() < averagePay * 0.8) {
        report.append(e).append(System.lineSeparator());
      }
    }

    //Overpaid employees header
    report.append("OVERPAID EMPLOYEES").append(System.lineSeparator());

    //overpaid employees
    for(EmployeeDto e : employeeDtos) {
      if(e.getPay() > averagePay * 1.2) {
        report.append(e).append(System.lineSeparator());
      }
    }

    //footer
    report.append("=== END ===").append(System.lineSeparator());

    //print the report
    System.out.println(report);
  }
}
