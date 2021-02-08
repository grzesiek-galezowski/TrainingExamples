using System.Collections.Generic;

namespace XUnitTestPatterns._07_FixtureManagement
{
  public class InMemoryLog
  {
    private readonly List<string> _entries = new();

    public void LogUser(User user)
    {
      _entries.Add("user: " + user.Name);
    }

    public List<string> RetrieveEntries()
    {
      return _entries;
    }
  }
}
