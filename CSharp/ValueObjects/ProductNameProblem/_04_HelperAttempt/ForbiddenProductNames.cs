using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductNameProblem._04_HelperAttempt
{
  public class ForbiddenProductNames
  {
    private readonly List<Tuple<string, ProductTypes>> _names = new List<Tuple<string, ProductTypes>>();

    public bool Contain(string name, ProductTypes type)
    {
      return _names.Any(n => ProductNameHelper.AreProductNamesEqual(n.Item2, n.Item1, type, name));
    }

    public void Add(string name, ProductTypes type)
    {
      _names.Add(Tuple.Create(name, type));
    }
  }
}