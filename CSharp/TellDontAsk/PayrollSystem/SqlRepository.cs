using System;
using System.Collections.Generic;

namespace PayrollSystem
{
  public class SqlRepository : IDisposable
  {
    public void Dispose()
    {
      Console.WriteLine("Disposing");
    }

    public IEnumerable<Employee> CurrentEmployees()
    {
      return new[] { new Employee(), new Employee(), new Employee() };
    }
  }

}

