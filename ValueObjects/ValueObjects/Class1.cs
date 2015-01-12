using System;
using System.Collections.Generic;

namespace ValueObjects
{
  public class Class1
  {

  }

  public class ApplicationApi
  {
    private readonly List<string> _existingNames;
    private IReport _report;
    private Dictionary<string, decimal> _names = new Dictionary<string, decimal>();

    public ApplicationApi(List<string> existingNames, IReport report)
    {
      _existingNames = existingNames;
      _report = report;
    }


    public void Add(string name, decimal price)
    {
      if (_names.ContainsKey(name))
      {
        _report.Error(name, price);
      }

      _names.Add(Tuple.Create(name, price));
    }

    public void Remove(string name)
    {
      _names.Remove(name);
    }


  }

  public interface IReport
  {
    void Error(string name, decimal price);
  }
}
