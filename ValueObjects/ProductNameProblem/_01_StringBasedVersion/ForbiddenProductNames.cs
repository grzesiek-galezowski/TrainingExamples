using System.Collections.Generic;
using System.Linq;

namespace ProductNameProblem._01_StringBasedVersion
{
  public class ForbiddenProductNames
  {
    private List<string> _names = new List<string>();

    public bool Contain(string name)
    {
      return _names.Any(n => n == name);
    }

    public void Add(string name)
    {
      _names.Add(name);
    }
  }

}