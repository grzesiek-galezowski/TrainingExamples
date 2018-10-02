using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatedRefactorings._5_MakeNotStaticFavouriteTrick
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
      var employeeDtos = Database.LoadEmployees();
      var report = new StringBuilder("");
      report.Append("=== BEGIN ===").Append(Environment.NewLine);

      report.Append("UNDERPAID EMPLOYEES").Append(Environment.NewLine);

      //underpaid employees
      foreach (EmployeeDto e in employeeDtos) 
      {
        if (e.Pay < averagePay * 0.8)
        {
          report.Append(e).Append(Environment.NewLine);
        }
      }

      report.Append("OVERPAID EMPLOYEES").Append(Environment.NewLine);

      //overpaid employees
      foreach (EmployeeDto e in employeeDtos) 
      {
        if (e.Pay > averagePay * 1.2)
        {
          report.Append(e).Append(Environment.NewLine);
        }
      }

      report.Append("=== END ===").Append(Environment.NewLine);

      Console.WriteLine(report.ToString());
    }
  }
}
