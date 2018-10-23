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
    private readonly int _averagePay;

    public EmployeeReportCommand(int averagePay)
    {
      this._averagePay = averagePay;
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
    //7. Make Non Static
    public void Execute()
    {
      var employeeDtos = Database.LoadEmployees();
      var report = new StringBuilder("");
      report.Append("=== BEGIN ===").Append(Environment.NewLine);

      report.Append("UNDERPAID EMPLOYEES").Append(Environment.NewLine);

      //underpaid employees
      foreach (var employee in employeeDtos) 
      {
        if (employee.Pay < _averagePay * 0.8)
        {
          report.Append(employee).Append(Environment.NewLine);
        }
      }

      report.Append("OVERPAID EMPLOYEES").Append(Environment.NewLine);

      //overpaid employees
      foreach (var employee in employeeDtos) 
      {
        if (employee.Pay > _averagePay * 1.2)
        {
          report.Append(employee).Append(Environment.NewLine);
        }
      }

      report.Append("=== END ===").Append(Environment.NewLine);

      Console.WriteLine(report.ToString());
    }
  }
}
