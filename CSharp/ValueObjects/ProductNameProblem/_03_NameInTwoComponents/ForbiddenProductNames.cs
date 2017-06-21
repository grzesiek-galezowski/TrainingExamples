using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductNameProblem._03_CaseInsensitive
{
  public class ForbiddenProductNames
  {
    private readonly List<Tuple<string, ProductTypes>> _names = new List<Tuple<string, ProductTypes>>();

    public bool Contain(string name, ProductTypes type)
    {
      return _names.Any(n => string.Equals(n.Item1, name, StringComparison.InvariantCultureIgnoreCase) &&
        n.Item2 == type);
    }

    public void Add(string name, ProductTypes type)
    {
      _names.Add(Tuple.Create(name, type));
    }
  }
}