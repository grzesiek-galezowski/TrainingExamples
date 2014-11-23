 using System;
using System.Collections.Generic;

namespace PayrollSystem
{
	public class Employee
	{
    public void Save()
    {
      Console.WriteLine("Saving");
    }

    public void SetBonusForYear(int i, decimal oneTimeBonus)
    {
      Console.WriteLine(string.Format("Setting for {0} to {1}", i, oneTimeBonus));
    }

    public int GetYearseOfService()
    {
      return 123;
    }

    public void SetSalary(decimal newSalary)
    {
      Console.WriteLine("New Salary is: " + newSalary);
    }

    public PayGrade GetPayGrade()
    {
      return new PayGrade();
    }

    public decimal GetSalary()
    {
      return new decimal(44.4);
    }
	}


}

