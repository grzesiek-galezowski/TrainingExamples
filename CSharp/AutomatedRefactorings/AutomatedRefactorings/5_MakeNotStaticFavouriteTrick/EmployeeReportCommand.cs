using System;
using System.Collections.Generic;
using System.Text;
using static System.Environment;

namespace OpenChatSpecification
{
  public class Database
  {
    public static List<EmployeeDto> LoadEmployees()
    {
      return new List<EmployeeDto>();
    }
  }

  public class EmployeeDto
  {
    public EmployeeDto(int pay)
    {
      this.Pay = pay;
    }

    public int Pay { get; }
  }

  public class EmployeeReportCommand
  {
    private int averagePay;

    public EmployeeReportCommand(int averagePay)
    {
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
    public void Execute()
    {
      List<EmployeeDto> employeeDtos = Database.LoadEmployees();
      StringBuilder report = new StringBuilder("");
      report.Append("=== BEGIN ===").Append(NewLine);

      report.Append("UNDERPAID EMPLOYEES").Append(NewLine);

      //underpaid employees
      foreach (EmployeeDto e in employeeDtos) 
      {
        if (e.Pay < averagePay * 0.8)
        {
          report.Append(e).Append(NewLine);
        }
      }

      report.Append("OVERPAID EMPLOYEES").Append(NewLine);

      //overpaid employees
      foreach (EmployeeDto e in employeeDtos) 
      {
        if (e.Pay > averagePay * 1.2)
        {
          report.Append(e).Append(NewLine);
        }
      }

      report.Append("=== END ===").Append(NewLine);

      Console.WriteLine(report.ToString());
    }
  }
}
