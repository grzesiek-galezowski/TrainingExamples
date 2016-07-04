using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace ProductNameProblem._02_CaseInsensitive
{
  public class ForbiddenProductNames
  {
    private readonly List<string> _names = new List<string>();

    public bool Contain(string name)
    {
      return _names.Any(n => string.Equals(n, name, StringComparison.InvariantCultureIgnoreCase));
    }

    public void Add(string name)
    {
      _names.Add(name);
    }
  }
}