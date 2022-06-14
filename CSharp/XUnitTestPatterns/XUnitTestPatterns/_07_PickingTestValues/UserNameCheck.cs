using System.Text.RegularExpressions;

namespace XUnitTestPatterns._07_PickingTestValues
{
  public class UserNameCheck
  {
    public bool PassesFor(string nameString)
    {
      return Regex.IsMatch(nameString, "[A-Z][A-Za-z]+");
    }
  }
}