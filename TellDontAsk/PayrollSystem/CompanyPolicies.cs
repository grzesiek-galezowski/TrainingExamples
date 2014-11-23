using System;

namespace PayrollSystem
{
  //TODO refactor this class to add new employee type: contractor
  //|---------------------|------------------------------|---------------------------|
  //| Employee Type       | Raise                        |     Bonus                 |
  //|---------------------|------------------------------|---------------------------|
  //| Regular Employee    | +10% of current salary       | +200% of current salary   |
  //|                     | if not reached maximum       | one time after five years |
  //|                     | on a given pay grade         |                           |
  //|---------------------|------------------------------|---------------------------|
  //| Contractor          | +5% of average salary        | +10% of current salary    |
  //|                     | calculated for last 3        | when a contractor receives|
  //|                     | years of service (or         | score more than 100 for   |
  //|                     | all previous years of        | the previous year         |
  //|                     | service if they have         |                           |
  //|                     | worked for less than 3 years |                           |


  public class CompanyPolicies : IDisposable
  {
    readonly SqlRepository _repository = new SqlRepository();

    public void ApplyYearlyIncentivePlan()
    {
      var employees = _repository.CurrentEmployees();
     
      foreach(var employee in employees)
      {
        var payGrade = employee.GetPayGrade();

        //evaluate raise
        if(employee.GetSalary() < payGrade.Maximum)
        {
          var newSalary = decimal.Add(employee.GetSalary(), decimal.Multiply(employee.GetSalary(), new decimal(0.1)));
             
          employee.SetSalary(newSalary);
        }
    
        //evaluate one-time bonus
        if(employee.GetYearsOfService() == 5)
        {
          var oneTimeBonus = employee.GetSalary() * 2;
          employee.SetBonusForYear(2014, oneTimeBonus);
        }

        employee.Save();
      }
    }

    public void Dispose()
    {
      _repository.Dispose();
    }
  }
}

